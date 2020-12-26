using Animals.Enum;
using Animals.Interfaces;
using System;

namespace Animals
{
    public class Animal : ISoundable
    {
        string name;
        int age;
        GenderEnum gender;

        public Animal(string name, int age, GenderEnum gender)
        {
            try
            {
                if (name!=null&&name!=""&&age>-1)
                {
                    Name = name;
                    Age = age;
                    Gender = gender;
                }
                else
                {
                    throw new ArgumentException("Invalid input!");
                }

            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid input!");
            }
        }

        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
        public GenderEnum Gender { get => gender; set => gender = value; }

        public virtual string ProduceSound()
        {
            return "";
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}{System.Environment.NewLine}{this.Name} {this.Age} {this.Gender}{System.Environment.NewLine}{ProduceSound()}";
        }
    }
}
