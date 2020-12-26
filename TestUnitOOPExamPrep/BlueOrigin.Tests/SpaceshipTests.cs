namespace BlueOrigin.Tests
{
    using System;
    using NUnit.Framework;

    public class SpaceshipTests
    {
        private Spaceship iss;
        [SetUp]
        public void SetUp()
        {
            this.iss = new Spaceship("ISS", 10);
        }

        [Test]
        public void CheckIfInitializeCorrectly()
        {
            string expectedName = "ISS";
            Assert.AreEqual(expectedName,iss.Name);
            Assert.AreEqual(0, iss.Count);
        }

        [Test]
        public void Name_ShouldThrowAnExcIfNullOrEmpty()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new Spaceship(null, 10));
        }

        [Test]
        public void Name_CorrectFlow_Setter()
        {
            string nameExpected = "SS";
            int capacityExp = 10;
            this.iss = new Spaceship("SS", 10);

            Assert.AreEqual(nameExpected, iss.Name);
            Assert.AreEqual(capacityExp, iss.Capacity);
        }

        [Test]
        public void CapacitySetter_WithLessThanZero_ThrowExc()
        {
            var ex = Assert.Throws<ArgumentException>(
                ()=>new Spaceship("ISS", -10));
            Assert.AreEqual("Invalid capacity!",ex.Message);
        }

        [Test]
        public void CapacitySetter_OverFlow()
        {
            this.iss = new Spaceship("ISS", 10);
            this.iss.Add(new Astronaut("Pesho", 45));
            this.iss.Add(new Astronaut("Mesho", 45));
            this.iss.Add(new Astronaut("Lesho", 45));
            this.iss.Add(new Astronaut("Resho", 45));
            this.iss.Add(new Astronaut("Xesho", 45));
            this.iss.Add(new Astronaut("Zesho", 45));
            this.iss.Add(new Astronaut("Uesho", 45));
            this.iss.Add(new Astronaut("Tesho", 45));
            this.iss.Add(new Astronaut("Qesho", 45));
            this.iss.Add(new Astronaut("Vesho", 45));

            var ex = Assert.Throws<InvalidOperationException>(() => this.iss.Add(new Astronaut("Eesho", 45)));
        }

        [Test]
        public void CapacitySetter_NormalFlow()
        {
            this.iss = new Spaceship("ISS", 10);

            int expectedCapacity = 10;
            Assert.AreEqual(expectedCapacity, iss.Capacity);
        }

        [Test]
        public void AddAustronautAboveCapacity_ThrowExc()
        {
            this.iss = new Spaceship("ISS", 1);
            this.iss.Add(new Astronaut("Lesho", 25));
            var ex = Assert.Throws<InvalidOperationException>(
                () => this.iss.Add(new Astronaut("Pesho", 45)));
            Assert.AreEqual("Spaceship is full!", ex.Message);
        }

        [Test]
        public void AddAlreadyExistingAustronaut_ThrowExc()
        {
            string nameAustronaut = "Pesho";
            this.iss.Add(new Astronaut(nameAustronaut, 25));

            var ex = Assert.Throws<InvalidOperationException>(
                () => this.iss.Add(new Astronaut(nameAustronaut, 45)));
            Assert.AreEqual($"Astronaut {nameAustronaut} is already in!", ex.Message);
        }

        [Test]
        public void AddAustronautCorrectly()
        {
            int expectedCountAustronaut = 1;

            this.iss.Add(new Astronaut("Pesho", 45));

            int austronautActualCount = this.iss.Count;

            Assert.AreEqual(expectedCountAustronaut, austronautActualCount);
        }

        [TestCase(null)]
        [TestCase("Lesho")]
        public void RemoveMethodWithNullOrNonExistingName_ReturnFalse(string name)
        {
            this.iss.Add(new Astronaut("Pesho", 45));

            Assert.IsFalse(this.iss.Remove(null));
        }

        [Test]
        public void RemoveMethodSuccesfully()
        {
            this.iss.Add(new Astronaut("Pesho", 45));

            Assert.IsTrue(this.iss.Remove("Pesho"));
        }
    }
}