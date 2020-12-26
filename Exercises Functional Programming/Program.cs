using System;
using System.Linq;

namespace Exercises_Functional_Programming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x=>Console.WriteLine(x));
        }
    }
}
