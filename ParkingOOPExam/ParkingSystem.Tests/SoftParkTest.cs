namespace ParkingSystem.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class SoftParkTest
    {
        private SoftPark softPark;

        [SetUp]
        public void Setup()
        {
            this.softPark = new SoftPark();
        }

        [Test]
        public void CheckConstructorCorrectFlow()
        {
            var parking = new Dictionary<string, Car>
            {
                {"A1", null},
                {"A2", null},
                {"A3", null},
                {"A4", null},
                {"B1", null},
                {"B2", null},
                {"B3", null},
                {"B4", null},
                {"C1", null},
                {"C2", null},
                {"C3", null},
                {"C4", null},
            };

            CollectionAssert.AreEqual
                (parking, softPark.Parking);

            CollectionAssert.AreEqual
                (parking.Values, softPark.Parking.Values);
            Assert.AreEqual(parking.Count, softPark.Parking.Count);
        }

        [Test]
        public void ParkCarOnNonExistingSpot_ThrowExc()
        {
            Car car = new Car("BMW", "VX443");

            var ex = Assert.Throws<ArgumentException>(
                () => softPark.ParkCar("F3", car));
            Assert.AreEqual("Parking spot doesn't exists!", ex.Message);
        }

        [Test]
        public void ParkOnOccupiedSpot_ThrowExc()
        {
            Car car = new Car("BMW", "VX443");

            softPark.ParkCar("A1",car);

            var ex = Assert.Throws<ArgumentException>(
                () => softPark.ParkCar("A1", new Car("Audi","RT434")));
            Assert.AreEqual("Parking spot is already taken!", ex.Message);
        }

        [Test]
        public void ParkAlreadyParkedCar_ThrowExc()
        {
            Car car = new Car("BMW", "VX443");

            softPark.ParkCar("B2", car);

            var ex = Assert.Throws<InvalidOperationException>(
                () => softPark.ParkCar("C1", car));
            Assert.AreEqual("Car is already parked!", ex.Message);
        }

        [Test]
        public void ParkCarSuccsesful()
        {
            Car car = new Car("BMW", "VX443");

            var expectedSpot = car;

            softPark.ParkCar("A2",car);

            var actual = softPark.Parking["A2"];

            Assert.AreEqual(expectedSpot,actual);
            Assert.AreEqual($"Car:FRE332 parked successfully!", softPark.ParkCar("A1", new Car("Audi","FRE332")));
        }

        [Test]
        public void RemoveCarFromNonExistingSpot_ThrowExc()
        {
            Car car = new Car("BMW", "VX443");

            var ex = Assert.Throws<ArgumentException>(() => softPark.RemoveCar("V3",car));
            Assert.AreEqual("Parking spot doesn't exists!",ex.Message);
        }

        [Test]
        public void RemoveDifferentCarFromTheSpot_ThrowExc()
        {
            Car car = new Car("BMW", "VX443");
            Car car2 = new Car("Audi", "LKI43");

            softPark.ParkCar("A2", car);

            var ex = Assert.Throws<ArgumentException>(
                () => softPark.RemoveCar("A2", car2));
            Assert.AreEqual("Car for that spot doesn't exists!", ex.Message);
        }

        [Test]
        public void RemoveSuccsesful()
        {
            Car car = new Car("BMW", "VX443");
            Car car2 = new Car("Audi", "KLIU34");

            softPark.ParkCar("A1", car);
            softPark.ParkCar("A3", car2);

            softPark.RemoveCar("A1", car);

            Assert.AreEqual(null, softPark.Parking["A1"]);

            Assert.AreEqual("Remove car:KLIU34 successfully!",softPark.RemoveCar("A3",car2));
        }
    }
}