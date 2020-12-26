using System;
using ValidationAttributes.Models;
using ValidationAttributes.Validators;

namespace ValidationAttributes
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var person = new Person
             (
                 "Bobi",
                 33
             );

            bool isValidEntity = Validator.IsValid(person);

            Console.WriteLine(new string('.', 77));

            Console.WriteLine(isValidEntity);
        }
    }
}
