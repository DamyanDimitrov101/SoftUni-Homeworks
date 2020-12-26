using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem_8._Custom_Comparator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> arr = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            Func<int, bool> Even = x => x % 2 == 0;

            
            Console.Write(string.Join(" ", arr.Where(x => Even(x)).OrderBy(x => x).ToList()));
            Console.Write(" ");
            Console.WriteLine(string.Join(" ", arr.Where(x => !Even(x)).OrderBy(x => x).ToList()));
        }
    }
}
