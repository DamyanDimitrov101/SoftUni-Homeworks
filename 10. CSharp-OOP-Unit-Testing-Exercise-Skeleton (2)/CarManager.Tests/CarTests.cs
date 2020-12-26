using NUnit.Framework;

namespace Tests
{
    using CarManager;
    using System;

    public class CarTests
    {
        private Car car;
        [SetUp]
        public void Setup()
        {
            car = new Car("BMW", "X3", 11, 63);
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            Assert.IsNotNull(this.car);
        }

        [TestCase("BMW","X3",11,63)]
        public void TestConstructorsForProperlyInitializing(string make,string model,double fuelConsumption,double fuelCapacity)
        {
            car = new Car(make, model, fuelConsumption, fuelCapacity);

            string makeExpected = make;
            string modelExpected = model;
            double fuelConsumptionExpected = fuelConsumption;
            double fuelCapacityExpected = fuelCapacity;
            double fuelAmountExpected = 0;

            Assert.AreEqual(makeExpected, car.Make);
            Assert.AreEqual(modelExpected, car.Model);
            Assert.AreEqual(fuelConsumptionExpected, car.FuelConsumption);
            Assert.AreEqual(fuelCapacityExpected, car.FuelCapacity);
            Assert.AreEqual(fuelAmountExpected, car.FuelAmount);
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestMakeSetter_WithNullOrEmpty(string make)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Car(make, "X3", 11, 63));

            Assert.AreEqual("Make cannot be null or empty!",ex.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestModelSetter_WithNullOrEmpty(string model)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Car("BMW", model, 11, 63));

            Assert.AreEqual("Model cannot be null or empty!", ex.Message);
        }

        [TestCase(-544)]
        [TestCase(0)]
        public void TestFuelConsumptionSetter_WithLessThanZeroOrZero(int fuelConsumption)
        {
            var ex = Assert.Throws<ArgumentException>
                (() => new Car("BMW", "X3", fuelConsumption, 63));
            Assert.AreEqual("Fuel consumption cannot be zero or negative!",ex.Message);
        }

        [TestCase(-544)]
        [TestCase(0)]
        public void TestFuelCapacitySetter_WithLessThanZeroOrZero(int fuelCapacity)
        {
            var ex = Assert.Throws<ArgumentException>
                (() => new Car("BMW", "X3", 11, fuelCapacity));
            Assert.AreEqual("Fuel capacity cannot be zero or negative!", ex.Message);
        }

        [Test]
        public void FuelAmountSetterShouldBeZeroAtInitializing()
        {
            double expectedAmount = 0;
            double actualAmount = car.FuelAmount;

            Assert.AreEqual(expectedAmount, actualAmount);
        }

       [TestCase(10)]
       public void RefuelMethodNormally(double fuelToRefuel)
        {
            car.Refuel(fuelToRefuel);
            double fuelAmountExpected = fuelToRefuel;

            Assert.AreEqual(fuelAmountExpected,car.FuelAmount);
        }

        [TestCase(-45)]
        [TestCase(0)]
        public void ThrowExceptionIfTryToRefuelMethod_WithLessThanZeroOrZero(int fuelToRefuel)
        {
            var ex = Assert.Throws<ArgumentException>
                (() => car.Refuel(fuelToRefuel));
            Assert.AreEqual("Fuel amount cannot be zero or negative!", ex.Message);
        }

        [Test]
        public void ThrowExceptionIfTryToRefuelMethod_WithMoreFuelNeedThanTheCapacityOfTheCar()
        {
            car.Refuel(100);

            var expectedFuelAmount = car.FuelCapacity;

            Assert.AreEqual(expectedFuelAmount,car.FuelAmount);
        }

        [Test]
        public void TestDrivingCorrectly()
        {
            this.car.Refuel(100);
            this.car.Drive(100);
            double expectedFuel = 52;

            Assert.AreEqual(expectedFuel, this.car.FuelAmount);
        }

        [Test]
        public void ThrowExceptionIfTryToDriveMethod_WithMoreFuelNeedThanTheAmountInTheCar()
        {
            car.Refuel(10);

            var ex = Assert.Throws<InvalidOperationException>
                (() => car.Drive(1111));
            Assert.AreEqual("You don't have enough fuel to drive!", ex.Message);
        }
    }
}