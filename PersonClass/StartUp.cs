using System;

namespace Person
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            try
            {
                Person person = new Person(input,age);
                System.Console.WriteLine(person);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }
    }
}