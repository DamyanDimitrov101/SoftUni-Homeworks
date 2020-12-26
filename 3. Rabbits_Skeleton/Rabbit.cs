using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbits
{
    public class Rabbit 
    {
        string name;
        string species;
        bool available = true;

        public Rabbit(string name, string species)
        {
            Name = name;
            Species = species;
        }

        public string Name { get => name; set => name = value; }
        public string Species { get => species; set => species = value; }
        public bool Available { get => available; set => available = value; }

        


        public override string ToString()
        {
            return $"Rabbit ({this.Species}): {this.Name}"; 
        }
    }
}
