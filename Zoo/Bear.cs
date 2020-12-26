using System;
using System.Collections.Generic;
using System.Text;

namespace Zoo
{
    public class Bear : Mammal
    {
        public Bear(string name) : base(name)
        {
            this.Name = name;
        }

        public new  string Name { get; set; }
    }
}
