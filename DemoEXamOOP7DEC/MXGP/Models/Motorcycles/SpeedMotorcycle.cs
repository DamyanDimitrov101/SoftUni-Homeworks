using MXGP.Models.Motorcycles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles
{
    public class SpeedMotorcycle : Motorcycle
    {
        private const int    speed_minimumHorsePower = 50;
        private const int    speed_maximumHorsePower = 69;
        private const double speed_cubicCentimeters = 125;

        private int horsePower;

        public SpeedMotorcycle(string model, int horsepower)
            : base(model, horsepower, speed_cubicCentimeters)
        {
        }

        public override int HorsePower
        {
            get => this.horsePower;
            set
            {
                if (value < speed_minimumHorsePower || value > speed_maximumHorsePower)
                {
                    throw new ArgumentException($"Invalid horse power: {value}.");
                }
                this.horsePower = value;
            }
        }

        public override double CubicCentimeters => speed_cubicCentimeters;

    }
}
