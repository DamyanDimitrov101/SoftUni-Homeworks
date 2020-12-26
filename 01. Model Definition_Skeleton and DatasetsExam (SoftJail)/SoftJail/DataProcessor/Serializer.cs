namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using ProductShop.XMLTools;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Linq;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            //            "Id": 3,
            //    "Name": "Binni Cornhill",
            //    "CellNumber": 503,
            //    "Officers": [
            //      {
            //        "OfficerName": "Hailee Kennon",
            //        "Department": "ArtificialIntelligence"
            //      },
            //      {
            //        "OfficerName": "Theo Carde",
            //        "Department": "Blockchain"
            //      }
            //    ],
            //    "TotalOfficerSalary": 7127.93
            //  },
            //


            var prisoners = context.Prisoners
                .Where(p => ids.Any(i => p.Id == i))
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(of => new
                    {
                        OfficerName = of.Officer.FullName,
                        Department = of.Officer.Department.Name
                    })
                    .OrderBy(of => of.OfficerName)
                    .ToArray(),
                    TotalOfficerSalary = (Decimal.Round(p.PrisonerOfficers.Sum(of => of.Officer.Salary), 2))
                })
                .OrderBy(p=>p.Name)
                .ThenBy(p=>p.Id)
                .ToArray();

            return JsonConvert.SerializeObject(prisoners, Formatting.Indented);
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var prisNamesArr = prisonersNames.Split(",").ToArray();

            var prisoners = context.Prisoners
                .ToArray()
                .Where(p => prisNamesArr.Any(pn => pn == p.FullName))
                .Select(p => new Prisoner_Export_DTO
                {
                     Name = p.FullName,
                      Id = p.Id,
                       IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                        EncryptedMessages = p.Mails
                        .ToArray()
                        .Select(m=> new Message
                        {
                             Description = new string(m.Description.Reverse().ToArray())
                        })
                        .ToArray()
                })
                .OrderBy(p=>p.Name)
                .ThenBy(p=>p.Id)
                .ToArray();

            return XMLConverter.Serialize(prisoners, "Prisoners");
        }

        //<Prisoner>
        //    <Id>3</Id>
        //    <Name>Binni Cornhill</Name>
        //    <IncarcerationDate>1967-04-29</IncarcerationDate>
        //    <EncryptedMessages>
        //      <Message>
        //        <Description>!?sdnasuoht evif-ytnewt rof deksa uoy ro orez artxe na ereht sI</Description>
        //      </Message>
        //    </EncryptedMessages>
        //  </Prisoner>
        //
    }
}