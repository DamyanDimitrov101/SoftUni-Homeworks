using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    public class Child : Person
    {
        public Child(string name, int age) : base(null, 0)
        {
            if (age<16)
            {
                base.Name = name;
                base.Age = age;
            }
            else
            {
                throw new ArgumentException("Age must be less than 16");
            }
        }
    }
}
