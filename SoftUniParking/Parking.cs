using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftUniParking
{
    public class Parking
    {
        List<Car> cars;
        private int capacity;
        public Parking(int capacity)
        {
            this.capacity = capacity;
            Cars = new List<Car>(capacity);
        }

        public List<Car> Cars { get => cars; set => cars = value; }
        public int Count { get => Cars.Count; }
        public string AddCar(Car car)
        {
            if (Cars.Any(c=>c.RegNumber==car.RegNumber))
            {
                return "Car with that registration number, already exists!";
            }
            else if (Cars.Count>=capacity)
            {
                return "Parking is full!";
            }
            else
            {
                Cars.Add(car);
                return $"Successfully added new car {car.Make} {car.RegNumber}";
            }

        }

        public string RemoveCar(string regNum)
        {
            if (!Cars.Any(c=>c.RegNumber==regNum))
            {
                return "Car with that registration number, doesn't exist!";
            }
            else
            {
                var currentToRemove = Cars.FirstOrDefault(c => c.RegNumber == regNum);

                Cars.Remove(currentToRemove);

                return $"Successfully removed {currentToRemove.RegNumber}";
            }

        }

        public Car GetCar(string registrationNumber)
        {
            return Cars.FirstOrDefault(c => c.RegNumber == registrationNumber);
        }

        public void RemoveSetOfRegistrationNumber(List<string> registrationNumbers)
        {
            foreach (var regNumber  in registrationNumbers)
            {
                this.Cars.RemoveAll(c=>c.RegNumber==regNumber);
            }
        }
    }
}
