using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem_5._Applied_Arithmetics
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> arr = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            Func<int, int> Add = x => x + 1;
            Func<int, int> Subtract = x => x - 1;
            Func<int, int> MultiPly = x => x * 2;


            string input = Console.ReadLine()?.ToLower();
            while (input!="end")
            {
                switch (input)
                {
                    case "add":
                        arr=arr.Select(x => Add(x)).ToList();
                        break;
                    case "multiply":
                        arr=arr.Select(x=>MultiPly(x)).ToList();
                        break;
                    case "subtract":
                        arr=arr.Select(x => Subtract(x)).ToList();
                        break;
                    case "print":
                        Print(arr);
                        break;
                }
                input = Console.ReadLine()?.ToLower();
            }
        }

        private static void Print(List<int> arr)
        {
            Console.WriteLine(string.Join(" ",arr));
        }
    }
}
