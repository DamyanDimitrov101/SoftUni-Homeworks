using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    public class Person
    {
        private string name;
        private int age;
        
        public Person(string name, int age)
        {
            if (age>=0)
            {
                Name = name;
                Age = age;
            }
            else
            {
                throw new ArgumentException("Age must be Positive");
            }
        }

        public string Name { get => name; set => name = value; }
        public int Age { get => age; protected set => age = value; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Name: {this.Name}, Age: {this.Age}");
            return sb.ToString();
        }
    }
}
