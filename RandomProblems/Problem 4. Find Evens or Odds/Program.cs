using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem_4._Find_Evens_or_Odds
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] bounds = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            string command = Console.ReadLine();

            Func<int, bool> isEven = x => x % 2 == 0;

            List<int> listNums = new List<int>();

            for (int i = bounds[0]; i <= bounds[1]; i++)
            {
                listNums.Add(i);
            }

            List<int> result = new List<int>();
            if (command=="even")
            {
                result = listNums.Where(isEven).ToList();
            }
            if (command=="odd")
            {
                result = listNums.Where(x=>!isEven(x)).ToList();
            }

            Console.WriteLine(string.Join(" ",result));
        }
    }
}
