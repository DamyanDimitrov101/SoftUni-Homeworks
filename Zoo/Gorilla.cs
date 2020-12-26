using System;
using System.Collections.Generic;
using System.Text;

namespace Zoo
{
    public class Gorilla : Mammal
    {
        public Gorilla(string name) : base(name)
        {
            this.Name = name;
        }
        public new string Name { get; set; }
    }
}
