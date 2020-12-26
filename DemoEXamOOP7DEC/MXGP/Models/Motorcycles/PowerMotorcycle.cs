using MXGP.Models.Motorcycles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles
{
    public class PowerMotorcycle : Motorcycle
    {
        private const int power_minimumHorsePower = 70;
        private const int power_maximumHorsePower = 100;
        private const double power_cubicCentimeters = 450;

        private int horsePower;

        public PowerMotorcycle(string model, int horsepower)
            : base(model, horsepower, power_cubicCentimeters)
        {
        }

        public override int HorsePower
        {
            get => this.horsePower;
            set 
            {
                if (value<power_minimumHorsePower||value>power_maximumHorsePower)
                {
                    throw new ArgumentException($"Invalid horse power: {value}.");
                }
                this.horsePower = value;
            }
        }

        public override double CubicCentimeters => power_cubicCentimeters;
    }
}