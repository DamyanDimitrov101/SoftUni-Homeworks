using System;
using System.Collections.Generic;

namespace SpeedRacing
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            Dictionary<string,Car> cars = new Dictionary<string, Car>();

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string model = input[0];
                double fuelAmount = double.Parse(input[1]);
                double distanceTravelled = double.Parse(input[2]);

                var current = new Car(model, fuelAmount, distanceTravelled);
                cars.Add(model,current);
            }


            string[] arr = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            while (arr[0]!="End")
            {
                
                string command = arr[0];
                string model = arr[1];
                double amountKm = double.Parse(arr[2]);

                if (cars.ContainsKey(model))
                {
                    if (cars[model].CanMove(amountKm))
                    {
                        
                    }
                    else
                    {
                        Console.WriteLine("Insufficient fuel for the drive");
                    }
                }

                arr = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car.Value);
            }
        }
    }
}
