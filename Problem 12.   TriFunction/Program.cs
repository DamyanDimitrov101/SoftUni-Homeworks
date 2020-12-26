using System;
using System.Linq;

namespace Problem_12.___TriFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string, int, bool> returnIsBiggerOrEqual = (word, count) => word.Sum(x => x) >= count;

            Func<string[], Func<string, int, bool>, int, string> func = (array, isLargerFunc, totalSum) => array.FirstOrDefault(x => isLargerFunc(x, totalSum));

            int n = int.Parse(Console.ReadLine());

            string[] arr = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            
            string targetName = func(arr, returnIsBiggerOrEqual,n);
            Console.WriteLine(targetName);
        }

    }
}
