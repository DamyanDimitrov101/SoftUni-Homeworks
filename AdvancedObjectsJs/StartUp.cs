using System;

namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Vehicle vehicle = new Vehicle(50, 100); 
            Car car = new Car(50, 120);
            SportCar sportCar = new SportCar(150, 250);
            RaceMotorcycle raceMotorcycle = new RaceMotorcycle(55, 140);

            Console.WriteLine(vehicle.FuelConsumption);
            Console.WriteLine(car.FuelConsumption);
            Console.WriteLine(sportCar.FuelConsumption);
            Console.WriteLine(raceMotorcycle.FuelConsumption);
        }
    }
}
