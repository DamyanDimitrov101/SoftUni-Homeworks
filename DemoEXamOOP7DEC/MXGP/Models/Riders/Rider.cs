using MXGP.Models.Motorcycles.Contracts;
using MXGP.Models.Riders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Riders
{
    public class Rider : IRider
    {
        private string name;
        private IMotorcycle motorcycle;
        private int numberOfWins;

        public Rider(string name)
        {
            Name = name;
        }

        public string Name 
        {
            get => this.name; 
            private set 
            {
                if (string.IsNullOrEmpty(value)||value.Length<5)
                {
                    throw new ArgumentException($"Name {value} cannot be less than 5 symbols.");
                }
                this.name = value;
            }
        }

        public IMotorcycle Motorcycle => this.motorcycle;

        public int NumberOfWins => this.numberOfWins;

        public bool CanParticipate => this.Motorcycle != null;

        public void AddMotorcycle(IMotorcycle motorcycle)
        {
            if (motorcycle is null)
            {
                throw new ArgumentNullException(nameof(motorcycle), "Motorcycle cannot be null.");
            }

            this.motorcycle = motorcycle;
        }

        public void WinRace()
        {
            this.numberOfWins++;
        }
    }
}
