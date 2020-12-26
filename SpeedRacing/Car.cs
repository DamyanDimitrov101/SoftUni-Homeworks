using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedRacing
{
    public class Car
    {
        public Car()
        {

        }

        public Car(string model, double fuelAmount, double fuelConsumptionPerKm)
        {
            Model = model;
            FuelAmount = fuelAmount;
            FuelConsumptionPerKilometer = fuelConsumptionPerKm;
        }

        public string Model { get; set; }
        public double FuelAmount { get; set; }
        public double FuelConsumptionPerKilometer { get; set; }
        public double TravelledDistance { get; set; }

        public bool CanMove(double distance)
        {
            bool res = false;

            if (FuelAmount-(distance * FuelConsumptionPerKilometer) >=0)
            {
                res = true;
                FuelAmount -= (distance * FuelConsumptionPerKilometer);
                TravelledDistance += distance;
            }
           
            return res;
        }

        public override string ToString()
        {
            return $"{this.Model} {(this.FuelAmount):F2} {(int)this.TravelledDistance}";
        }
    }
}
