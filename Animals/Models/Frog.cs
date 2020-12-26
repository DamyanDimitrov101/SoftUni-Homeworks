using System;
using System.Collections.Generic;
using System.Text;
using Animals.Enum;
using Animals.Interfaces;

namespace Animals.Models
{
    public class Frog : Animal, ISoundable
    {
        public Frog(string name, int age, GenderEnum gender) : base(name, age, gender)
        {
        }

        public override string ProduceSound()
        {
            return "Ribbit";
        }
    }
}
