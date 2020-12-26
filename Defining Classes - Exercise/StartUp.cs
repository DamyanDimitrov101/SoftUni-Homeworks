using System;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Family family = new Family();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                

                Person current = new Person(input[0], int.Parse(input[1]));

                family.AddMember(current);
            }

            Person[] oldestMembers = family.OverThirtyYearsOld();
            foreach (var per in oldestMembers)
            {
                Console.WriteLine(per.Name+" - "+per.Age);
            }
        }
    }
}
