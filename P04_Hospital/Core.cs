using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P04_Hospital
{
    public class Core
    {
        public void Run()
        {
            Dictionary<string, List<string>> doctor = new Dictionary<string, List<string>>();
            Dictionary<string, List<List<string>>> departments = new Dictionary<string, List<List<string>>>();

            GetHospitalInfo(doctor, departments);

            PrintTheRequestedData(doctor, departments);
        }

        private static void PrintTheRequestedData(Dictionary<string, List<string>> doctor, Dictionary<string, List<List<string>>> departments)
        {
            string command = Console.ReadLine();

            while (command != "End")
            {
                string[] args = command.Split();

                if (args.Length == 1)
                {
                    Console.WriteLine(string.Join("\n", departments[args[0]].Where(x => x.Count > 0).SelectMany(x => x)));
                }
                else if (args.Length == 2 && int.TryParse(args[1], out int staq))
                {
                    Console.WriteLine(string.Join("\n", departments[args[0]][staq - 1].OrderBy(x => x)));
                }
                else
                {
                    Console.WriteLine(string.Join("\n", doctor[args[0] + args[1]].OrderBy(x => x)));
                }
                command = Console.ReadLine();
            }
        }

        private static void GetHospitalInfo(Dictionary<string, List<string>> doctor, Dictionary<string, List<List<string>>> departments)
        {
            string command = Console.ReadLine();
            while (command != "Output")
            {
                string[] tokens = command.Split();
                var department = tokens[0];
                var firstName = tokens[1];
                var secondName = tokens[2];
                var patient = tokens[3];
                var fullName = firstName + secondName;

                if (!doctor.ContainsKey(firstName + secondName))
                {
                    doctor[fullName] = new List<string>();
                }
                if (!departments.ContainsKey(department))
                {
                    departments[department] = new List<List<string>>();
                    for (int room = 0; room < 20; room++)
                    {
                        departments[department].Add(new List<string>());
                    }
                }

                bool hasSpace = departments[department].SelectMany(x => x).Count() < 60;
                if (hasSpace)
                {
                    int currentRoom = 0;
                    doctor[fullName].Add(patient);
                    for (int room = 0; room < departments[department].Count; room++)
                    {
                        if (departments[department][room].Count < 3)
                        {
                            currentRoom = room;
                            break;
                        }
                    }
                    departments[department][currentRoom].Add(patient);
                }

                command = Console.ReadLine();
            }
        }
    }
}
