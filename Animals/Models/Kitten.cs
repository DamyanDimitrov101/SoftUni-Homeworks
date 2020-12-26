using System;
using System.Collections.Generic;
using System.Text;
using Animals.Enum;
using Animals.Interfaces;

namespace Animals.Models
{
    public class Kitten : Animal, ISoundable
    {
        private const GenderEnum genderKitten = GenderEnum.Female;
        public Kitten(string name, int age) : base(name, age, genderKitten)
        {
        }
        public Kitten(string name, int age, GenderEnum gender) : base(name, age, genderKitten)
        {
        }

        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
