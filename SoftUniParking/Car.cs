using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniParking
{
    public class Car : IComparable
    {
        string make;
        string model;
        int horsePower;
        string regNumber;

        public Car(string make, string model, int horsePower, string regNumber)
        {
            Make = make;
            Model = model;
            HorsePower = horsePower;
            RegNumber = regNumber;
        }

        public string Make { get => make; set => make = value; }
        public string Model { get => model; set => model = value; }
        public int HorsePower { get => horsePower; set => horsePower = value; }
        public string RegNumber { get => regNumber; set => regNumber = value; }

        public int CompareTo(object obj)
        {
            return RegNumber.CompareTo(obj);
        }

        public override string ToString()
        {
            return $"Make: {Make}{System.Environment.NewLine}Model: {Model}{System.Environment.NewLine}HorsePower: {HorsePower}{System.Environment.NewLine}RegistrationNumber: {RegNumber}";
        }
    }
}
