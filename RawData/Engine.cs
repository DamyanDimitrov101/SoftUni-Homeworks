using System;
using System.Collections.Generic;
using System.Text;

namespace RawData
{
    public class Engine
    {
        string model;
        int power;
        int displacement;
        string efficiency;

        public Engine(string model, int power)
        {
            Model = model;
            Power = power;
            Displacement = -1;
            Efficiency = null;
        }

        public Engine(string model, int power, int displacement) : this(model, power)
        {
            Displacement = displacement;
        }

        public Engine(string model, int power, string efficiency) : this(model, power)
        {
            Efficiency = efficiency;
        }

        public Engine(string model, int power, int displacement, string efficiency) : this(model, power, displacement)
        {
            Displacement = displacement;
            Efficiency = efficiency;
        }

        public string Model { get => model; set => model = value; }
        public int Power { get => power; set => power = value; }
        public int Displacement { get => displacement; set => displacement = value; }
        public string Efficiency { get => efficiency; set => efficiency = value; }
    }
}
