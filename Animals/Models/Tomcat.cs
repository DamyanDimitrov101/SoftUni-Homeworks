using System;
using System.Collections.Generic;
using System.Text;
using Animals.Enum;
using Animals.Interfaces;

namespace Animals.Models
{
    public class Tomcat : Animal, ISoundable
    {
        private const GenderEnum genderTomcat = GenderEnum.Male;
        public Tomcat(string name, int age) : base(name, age, genderTomcat)
        {
        }
        public Tomcat(string name, int age, GenderEnum gender) : base(name, age, genderTomcat)
        {
        }

        public override string ProduceSound()
        {
            return "MEOW";
        }
    }
}
