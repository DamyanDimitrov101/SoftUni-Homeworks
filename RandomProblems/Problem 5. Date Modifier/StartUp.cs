using System;

namespace DateModifier
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = Console.ReadLine();
            string b = Console.ReadLine();

            DateModifier dateModifier = new DateModifier();

            Console.WriteLine(dateModifier.GetDiff(a,b));
        }
    }
}
