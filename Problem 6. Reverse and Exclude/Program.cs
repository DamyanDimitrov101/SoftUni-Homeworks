using System;
using System.Linq;

namespace Problem_6._Reverse_and_Exclude
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int divisior = int.Parse(Console.ReadLine());

            Func<int, int, bool> notDivisible = (x, y) => x % y != 0;

            arr = arr.Reverse().Where(x=>notDivisible(x,divisior)).ToArray();

            Console.WriteLine(string.Join(" ",arr));
        }
    }
}
