using System;
using System.Collections.Generic;
using System.Linq;

namespace RawData
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();
            List<Engine> engines = new List<Engine>();


            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] arr = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string modelEng = arr[0];
                int power = int.Parse(arr[1]);
                int displacement = -1;
                Engine engine = new Engine(modelEng, power);
                if (arr.Length==2)
                {
                    engine = new Engine(modelEng, power);
                }
                else if (arr.Length==3)
                {
                    if (int.TryParse(arr[2],out displacement))
                    {
                    engine = new Engine(modelEng, power, displacement);
                    }
                    else
                    {
                    engine = new Engine(modelEng, power, arr[2]);
                    }
                }
                else if (arr.Length==4)
                {
                    engine = new Engine(modelEng, power, int.Parse(arr[2]), arr[3]);
                }

                engines.Add(engine);
            }

            int m = int.Parse(Console.ReadLine());

            for (int i = 0; i < m; i++)
            {
                string[] arr = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string model = arr[0];
                string engineModel = arr[1];
                Engine current = engines.FirstOrDefault(e => e.Model == engineModel);
                int weight = -1;
                string color = null;

                Car car = new Car(model, current);
                if (arr.Length==3)
                {
                    if (int.TryParse(arr[2],out weight))
                    {
                        car = new Car(model, current, weight);

                    }
                    else
                    {
                        color = arr[2];
                        car = new Car(model, current, color);
                    }
                }
                else if (arr.Length==4)
                {
                    car = new Car(model,current,int.Parse(arr[2]),arr[3]);
                }

                cars.Add(car);
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }
}