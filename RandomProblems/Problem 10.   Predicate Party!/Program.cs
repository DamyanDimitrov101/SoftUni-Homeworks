using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem_10.___Predicate_Party_
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> arrNames = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            Func<string, string, bool> startLetter = (name, le) => name.StartsWith(le);
            Func<string, string, bool> endLetter = (name, le) => name.EndsWith(le);
            Func<string, int, bool> length = (name, len) => name.Length == len;


            string input = Console.ReadLine();

            while (input != "Party!")
            {
                string[] token = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                List<string> reserve = new List<string>(arrNames);


                for (int i = 0; i < arrNames.Count; i++)
                {
                    switch (token[1])
                    {
                        case "StartsWith":

                            if (startLetter(arrNames[i], token[2]))
                            {
                                if (token[0] == "Double")
                                {
                                    reserve = MoveRigthAndDouble(reserve, i);
                                    continue;
                                }
                                if (token[0] == "Remove")
                                {
                                    reserve.Remove(arrNames[i]);
                                }
                            }
                            break;

                        case "EndsWith":
                            if (endLetter(arrNames[i], token[2]))
                            {
                                if (token[0] == "Double")
                                {
                                    reserve = MoveRigthAndDouble(reserve, i);
                                    continue;
                                }
                                if (token[0] == "Remove")
                                {
                                    reserve.Remove(arrNames[i]);
                                }
                            }

                            break;

                        case "Length":
                            if (length(arrNames[i], int.Parse(token[2])))
                            {
                                if (token[0] == "Double")
                                {
                                    reserve = MoveRigthAndDouble(reserve, i);

                                }
                                if (token[0] == "Remove")
                                {
                                    reserve.Remove(arrNames[i]);
                                }
                            }
                            break;
                    }
                }

                arrNames = reserve;

                input = Console.ReadLine();
            }

            if (arrNames.Count > 0)
            {
                Console.WriteLine(string.Join(", ", arrNames) + " are going to the party!");
            }
            else
            {
                Console.WriteLine("Nobody is going to the party!");
            }
        }

        private static List<string> MoveRigthAndDouble(List<string> arrNames, int index)
        {
            List<string> result = new List<string>(arrNames);
            result.Add("");
            for (int i = index; i < arrNames.Count; i++)
            {
                result[i + 1] = arrNames[i];
            }
            return result;
        }
    }
}
