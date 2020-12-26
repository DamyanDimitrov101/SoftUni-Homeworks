using System;
using System.Collections.Generic;
using System.Text;
using Animals.Enum;
using Animals.Models;

namespace Animals.Core
{
    public class Engine
    {
        public void Run()
        {
            Dictionary<string, Type> dicOfAnimals= new Dictionary<string, Type>()
            {
                { "Dog",typeof(Dog)},
                { "Cat", typeof(Cat)},
                { "Frog", typeof(Frog)},
                { "Kittens",typeof(Kitten)},
                { "Tomcat",typeof(Tomcat)},
            };

            string input = Console.ReadLine();
            while (input!= "Beast!")
            {
                string[] animalInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = animalInfo[0];
                int age = int.Parse(animalInfo[1]);
                GenderEnum gender = GenderEnum.Unknown;
                if (animalInfo.Length>2)
                {
                    if (animalInfo[2] == "Male")
                    {
                        gender = GenderEnum.Male;
                    }
                    if (animalInfo[2] == "Female")
                    {
                        gender = GenderEnum.Female;
                    }
                }

                try
                {
                    switch (input)
                    {
                        case "Dog":
                            Dog dog = new Dog(name, age, gender);
                            Console.WriteLine(dog);
                            break;
                        case "Cat":
                            if (input=="Cat")
                            {

                                Cat cat = new Cat(name, age,gender);
                                Console.WriteLine(cat);
                            }
                            else 
                            {
                                if (gender == GenderEnum.Male)
                                {

                                    Tomcat cat = new Tomcat(name, age, gender);
                                    Console.WriteLine(cat);
                                }
                                else
                                {
                                    Kitten cat = new Kitten(name, age, gender);
                                    Console.WriteLine(cat);
                                }
                            }
                            break;
                        case "Frog":
                            Frog frog = new Frog(name, age, gender);
                            Console.WriteLine(frog);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                input = Console.ReadLine();
            }
        }
    }
}
