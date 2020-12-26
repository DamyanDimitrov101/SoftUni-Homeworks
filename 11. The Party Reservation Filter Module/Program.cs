using System;
using System.Collections.Generic;
using System.Linq;

namespace _11._The_Party_Reservation_Filter_Module
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arr = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var filters = new List<KeyValuePair<string, string>>();

            string input = Console.ReadLine();
            ReceiveFilters(filters, input);
            arr = ApplyFilters(arr, filters);

            Console.WriteLine(string.Join(" ", arr));
        }
        private static string[] ApplyFilters(string[] arr, List<KeyValuePair<string, string>> filters)
        {
            foreach (var filterCurrent in filters)
            {
                switch (filterCurrent.Key)
                {
                    case "Starts with":
                        arr = arr.Where(x => !x.StartsWith(filterCurrent.Value)).ToArray();
                        break;
                    case "Ends with":
                        arr = arr.Where(x => !x.EndsWith(filterCurrent.Value)).ToArray();
                        break;
                    case "Length":
                        arr = arr.Where(x => x.Length != int.Parse(filterCurrent.Value)).ToArray();
                        break;
                    case "Contains":
                        arr = arr.Where(x => !x.Contains(filterCurrent.Value)).ToArray();
                        break;
                }
            }

            return arr;
        }

        private static void ReceiveFilters(List<KeyValuePair<string, string>> filters, string input)
        {
            while (input != "Print")
            {
                string[] token = input.Split(";", StringSplitOptions.RemoveEmptyEntries);
                string command = token[0];
                string filterType = token[1];

                switch (command)
                {
                    case "Add filter":
                       filters.Add(new KeyValuePair<string, string>(filterType, token[2]));
                        break;
                    case "Remove filter":
                        filters.Remove(new KeyValuePair<string, string>(filterType, token[2]));
                        break;
                }
                input = Console.ReadLine();
            }
        }
    }
}
