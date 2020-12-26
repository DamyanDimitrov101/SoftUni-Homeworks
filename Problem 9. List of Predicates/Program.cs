using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem_9._List_of_Predicates
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int[] arr = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Func<int,int,bool> divisible = (x, y) => x % y == 0;


            List<int> result = new List<int>();
            for (int i = 1; i <= n; i++)
            {
                bool isDiv= true;
                foreach (var num in arr)
                {
                    if (!divisible(i,num))
                    {
                        isDiv = false;
                    }
                }
                if (isDiv)
                {
                    result.Add(i);
                }
            }

            Console.WriteLine(string.Join(" ",result.OrderBy(x=>x)));
        }
    }
}
