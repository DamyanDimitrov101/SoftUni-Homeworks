using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using var context = new SoftUniContext();

            var result = RemoveTown(context);

            File.AppendAllText("../../../result.txt",result);
        }


        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            //Now we can use the SoftUniContext to extract data from our database. Your first task is to extract all employees and return their first, last and middle name, their job title and salary, rounded to 2 symbols after the decimal separator, all of those separated with a space. Order them by employee id.
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees.OrderBy(x => x.EmployeeId).ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            //Your task is to extract all employees with salary over 50000. Return their first names and salaries in format “{firstName} - {salary}”.Salary must be rounded to 2 symbols, after the decimal separator. Sort them alphabetically by first name.

            var employees = context.Employees
                .Where(e => e.Salary > 50000).Select(e => new
                {
                    FirstName = e.FirstName,
                    Salary = e.Salary
                }).OrderBy(e => e.FirstName).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            //Extract all employees from the Research and Development department. Order them by salary (in ascending order), then by first name (in descending order). Return only their first name, last name, department name and salary rounded to 2 symbols, after the decimal separator in the format shown below:

            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e=> new 
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DepartmentName = e.Department.Name,
                    Salary = e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from Research and Development - ${employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            //Create a new address with text "Vitoshka 15" and TownId 4. Set that address to the employee with last name "Nakov".
            //Then order by descending all the employees by their Address’ Id, take 10 rows and from them, take the AddressText. Return the results each on a new line:
            StringBuilder sb = new StringBuilder();

            var adress = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4 
            };

            var nakov = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            nakov.Address = adress;
            context.SaveChanges();

            var employees = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Select(x=> new 
                {
                    AdressText = x.Address.AddressText
                })
                .Take(10)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine(employee.AdressText);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            //Find the first 10 employees who have projects started in the period 2001 - 2003 (inclusive). Print each employee's first name, last name, manager’s first name and last name. Then return all of their projects in the format "--<ProjectName> - <StartDate> - <EndDate>", each on a new row. If a project has no end date, print "not finished" instead.

            var employees = context.Employees.Where(e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001
                && ep.Project.StartDate.Year <= 2003))
                .Select(s=> new
                {
                    EmployeeFirstName = s.FirstName,
                    EmployeeLastName = s.LastName,
                    ManagerFirstName = s.Manager.FirstName,
                    ManagerLastName = s.Manager.LastName,
                    Projects = s.EmployeesProjects.Select(p=> new
                    {
                        ProjectName = p.Project.Name,
                        StartDate= p.Project.StartDate,
                        EndDate = p.Project.EndDate
                    }).ToList()
                })
                .Take(10)
                .ToList();


            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.EmployeeFirstName} {employee.EmployeeLastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var pr in employee.Projects)
                {
                    var StartDate = pr.StartDate.ToString("M/d/yyyy h:mm:ss tt");
                    var EndDate = pr.EndDate.HasValue
                        ? pr.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt")
                        : "not finished";
                    
                    sb.AppendLine($"--{pr.ProjectName} - {StartDate} - {EndDate}");
                }       
            }

            return sb.ToString().Trim();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            //Find all addresses, ordered by the number of employees who live there (descending), then by town name (ascending), and finally by address text (ascending). Take only the first 10 addresses. For each address return it in the format "<AddressText>, <TownName> - <EmployeeCount> employees"

            var adresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .Select(a=>new
            {
                    a.AddressText,
                    TownName= a.Town.Name,
                    EmployeeCount = a.Employees.Count
            })
                .ToList();


            foreach (var adress in adresses)
            {
                sb.AppendLine($"{adress.AddressText}, {adress.TownName} - {adress.EmployeeCount} employees");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            //Get the employee with id 147. Return only his/her first name, last name, job title and projects (print only their names). The projects should be ordered by name (ascending). Format of the output.

            var employee = context.Employees.FirstOrDefault(e=> e.EmployeeId == 147);

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            var projects = context.EmployeesProjects
                .Where(e => e.EmployeeId == 147)
                .Select(e=> new
                {
                    ProjectName = e.Project.Name
                })
                .OrderBy(p=>p.ProjectName)
                .ToList();

            foreach (var pr in projects)
            {
                sb.AppendLine(pr.ProjectName);
            }

            return sb.ToString().Trim();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            //Find all departments with more than 5 employees. Order them by employee count (ascending), then by department name (alphabetically). 
            //For each department, print the department name and the manager’s first /and /last      name on  the  first row.
            //Then print the first name, the last name and the job title of every employee on a /new /row.
            //Order the employees by first name(ascending), then by last name(ascending).
            //Format of the output: For each department print it in the format "<DepartmentName> /-   /<ManagerFirstName>   <ManagerLastName>" and for each employee print it in /the /format   "<EmployeeFirstName>   <EmployeeFirstName> - <JobTitle>".

            var departments = context.Departments
                               .Where(d => d.Employees.Count > 5)
                               .OrderBy(d => d.Employees.Count)
                               .ThenBy(d => d.Name)
                               .Select(x => new
                               {
                                   DepartmentName = x.Name,
                                   ManagerFullName = x.Manager.FirstName + " " + x.Manager.LastName,
                                   Employees = x.Employees.Select(e => new
                                   {
                                       FirstName = e.FirstName,
                                       LastName = e.LastName,
                                       JobTitle = e.JobTitle
                                   })
                                   .OrderBy(e=>e.FirstName)
                                   .ThenBy(e=>e.LastName)
                                   .ToList()
                               })
                               .ToList();


            foreach (var dep in departments)
            {
                sb.AppendLine($"{dep.DepartmentName} - {dep.ManagerFullName}");

                foreach (var emp in dep.Employees)
                {
                    sb.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");
                }
            }

            return sb.ToString().Trim();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            //Write a program that return information about the last 10 started projects. Sort them by name lexicographically and return their name, description and start date, each on a new row. Format of the output
            //Use date format: "M/d/yyyy h:mm:ss tt".


            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p=>p.Name)
                .ToList();

            foreach (var pr in projects)
            {
                sb.AppendLine(pr.Name);
                sb.AppendLine(pr.Description);
                sb.AppendLine(pr.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture));
            }

            return sb.ToString().Trim();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            //Write a program that increase salaries of all employees that are in the Engineering, Tool Design, Marketing or Information Services department by 12%. Then return first name, last name and salary (2 symbols after the decimal separator) for those employees whose salary was increased. Order them by first name (ascending), then by last name (ascending). Format of the output.

            var employees = context.Employees
                .Where(e => e.Department.Name == "Engineering" 
                         || e.Department.Name == "Tool Design" 
                         || e.Department.Name == "Marketing" 
                         || e.Department.Name == "Information Services")
                .OrderBy(e=>e.FirstName)
                .ThenBy(e=>e.LastName)
                .ToList();

            foreach (var emp in employees)
            {
                emp.Salary *= 1.12M;
                sb.AppendLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:F2})");
            }

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            //Write a program that finds all employees whose first name starts with "Sa". Return their first, last name, their job title and salary, rounded to 2 symbols after the decimal separator in the format given in the example below. Order them by first name, then by last name (ascending).

            var employess = context.Employees
                .Where(e => e.FirstName.ToUpper().StartsWith("SA"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e=>e.FirstName)
                .ThenBy(e=>e.LastName)
                .ToList();
            if (employess.Count>0)
            {
                foreach (var emp in employess)
                {
                    sb.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:F2})");
                }
            }
            

            return sb.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            //Let's delete the project with id 2. Then, take 10 projects and return their names, each on a new line. Remember to restore your database after this task.

            var project = context.Projects.FirstOrDefault(p => p.ProjectId == 2);

            var employeeProjects = context.EmployeesProjects.Where(ep => ep.ProjectId == 2).ToList();

            context.EmployeesProjects.RemoveRange(employeeProjects);

            context.Projects.Remove(project);

            context.SaveChanges();

            var projects = context.Projects.Take(10).Select(p => new
            {
                p.Name
            })
                .ToList();

            foreach (var pr in projects)
            {
                sb.AppendLine(pr.Name);
            }

            return sb.ToString().Trim();
        }

        public static string RemoveTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            //Write a program that deletes a town with name „Seattle”. Also, delete all addresses that are in those towns. Return the number of addresses that were deleted in format “{count} addresses in Seattle were deleted”. There will be employees living at those addresses, which will be a problem when trying to delete the addresses. So, start by setting the AddressId of each employee for the given address to null. After all of them are set to null, you may safely remove all the addresses from the context.Addresses and finally remove the given town.

            var townSeattle = context.Towns
                .FirstOrDefault(t => t.Name == "Seattle");

            var addresses = context.Addresses
                .Where(a => a.Town.Name == "Seattle")
                .ToList();

            int numberOfAddressesDeleted = addresses.Count();

            var employees = context.Employees.Where(e => e.Address.Town.Name == "Seattle").ToList();

            foreach (var employee in employees)
            {
                employee.AddressId = null;
            }

            context.Addresses.RemoveRange(addresses);
            context.Towns.Remove(townSeattle);

            context.SaveChanges();

            sb.AppendLine($"{numberOfAddressesDeleted} addresses in Seattle were deleted");

            return sb.ToString().Trim();
        }
    }
}
