using System;
using System.Collections.Generic;
using System.Text;
using Animals.Enum;
using Animals.Interfaces;

namespace Animals.Models
{
    public class Cat : Animal, ISoundable
    {
        public Cat(string name, int age, GenderEnum gender) : base(name, age, gender)
        {
        }
        
        public override string ProduceSound()
        {
            return "Meow meow";
        }
    }
}
