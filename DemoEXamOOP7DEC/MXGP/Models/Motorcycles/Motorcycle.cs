using MXGP.Models.Motorcycles.Contracts;
using System;

namespace MXGP.Models.Motorcycles
{
    public abstract class Motorcycle : IMotorcycle
    {
        private string model;

        protected Motorcycle(string model,int horsepower,double cubicCentimeters)
        {
            this.Model = model;
        }

        public string Model
        {
            get => model;
            private set => this.model =
                (string.IsNullOrWhiteSpace(value)
                || value.Length < 4 ?
                throw new ArgumentException
                ($"Model {value} cannot be less than 4 symbols.")
                : value);
        }

        public abstract int HorsePower { get; set; }

        public abstract double CubicCentimeters { get; }


        public double CalculateRacePoints(int laps)
        {
            if (laps<=0)
            {
                return 0;
            }

            return this.CubicCentimeters/(this.HorsePower*laps);
        }


    }
}
