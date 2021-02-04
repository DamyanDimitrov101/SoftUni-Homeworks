using System;
using System.Linq;

namespace Problem_2._Knights_of_Honor
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x => Console.WriteLine($"Sir {x}"));
        }
    }
}
