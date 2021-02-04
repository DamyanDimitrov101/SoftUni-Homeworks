using System;
using System.Linq;

namespace Problem_3._Custom_Min_Function
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList().OrderBy(x => x).FirstOrDefault());

        }
    }
}
