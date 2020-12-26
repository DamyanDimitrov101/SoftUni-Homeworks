namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using ProductShop.XMLTools;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            List<Department> departments = new List<Department>();

            var departmentDTOs = JsonConvert.DeserializeObject<Department_Import_DTO[]>(jsonString);


            foreach (var dep in departmentDTOs)
            {
                if (!IsValid(dep))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var departmentValid = new Department()
                {
                    Name = dep.Name
                };

                if (dep.Cells.Length == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isWithInvalidCell = false;

                foreach (var cell in dep.Cells)
                {
                    if (!IsValid(cell))
                    {
                        isWithInvalidCell = true;
                        break;
                    }

                    var cellValid = new Cell()
                    {
                        CellNumber = cell.CellNumber,
                        HasWindow = cell.HasWindow
                    };

                    departmentValid.Cells.Add(cellValid);
                }

                if (isWithInvalidCell)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                departments.Add(departmentValid);
                sb.AppendLine($"Imported {departmentValid.Name} with {departmentValid.Cells.Count} cells");
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var prisoners = new List<Prisoner>();

            var prisonerDTOs = JsonConvert.DeserializeObject<Prisoner_Import_DTO[]>(jsonString);


            foreach (var pris in prisonerDTOs)
            {
                if (!IsValid(pris))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime incarcerationDate;
                bool isIncarnationDateValid = DateTime.TryParseExact(pris.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out incarcerationDate);

                DateTime releaseDate;
                bool isReleaseDateValid = DateTime.TryParseExact(pris.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);


                if (!isIncarnationDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (pris.Bail != null)
                {
                    if (pris.Bail < 0)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                }


                var prisionerValid = new Prisoner()
                {
                    FullName = pris.FullName,
                    Nickname = pris.Nickname,
                    Age = pris.Age,
                    Bail = 0,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = releaseDate,
                    CellId = pris.CellId
                };


                if (pris.Bail != null)
                {
                    prisionerValid.Bail = pris.Bail;
                }

                


                bool isWithIncorrectMails = false;

                foreach (var mail in pris.Mails)
                {
                    if (!IsValid(mail))
                    {
                        isWithIncorrectMails = true;
                        break;
                    }

                    var mailValid = new Mail()
                    {
                        Description = mail.Description,
                        Address = mail.Address,
                        Prisoner = prisionerValid,
                        Sender = mail.Sender
                    };

                    prisionerValid.Mails.Add(mailValid);
                }

                if (isWithIncorrectMails)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                prisoners.Add(prisionerValid);
                sb.AppendLine($"Imported {prisionerValid.FullName} {prisionerValid.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            List<Officer> officers = new List<Officer>();

            List<OfficerPrisoner> prisoners = new List<OfficerPrisoner>();

            var officersDTOs = XMLConverter.Deserializer<OfficerPrisoners_Import_DTO>(xmlString, "Officers");


            foreach (var off in officersDTOs)
            {
                if (!IsValid(off) || off.Money<0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                object position;
                bool isValidPosition = Enum.TryParse(typeof(Position), off.Position, out position);

                object weapon;
                bool isValidWeapon  = Enum.TryParse(typeof(Weapon), off.Weapon, out weapon);

                if (!isValidPosition || !isValidWeapon)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var officer = new Officer()
                {
                    FullName = off.FullName,
                    Salary = off.Money,
                    Position = (Position)position,
                    Weapon = (Weapon)weapon,
                    DepartmentId = off.DepartmentId,
                };

                foreach (var pris in off.Prisoners)
                {
                    var prisoner = context.Prisoners.FirstOrDefault(p => p.Id == pris.Id);



                    if (prisoner!=null)
                    {
                        var officerPrisoner = new OfficerPrisoner()
                        {
                            Officer = officer,
                            Prisoner = prisoner
                        };

                        prisoners.Add(officerPrisoner);

                        officer.OfficerPrisoners.Add(officerPrisoner);
                    }
                }

                officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(officers);
            context.OfficersPrisoners.AddRange(prisoners);
            context.SaveChanges();


            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}