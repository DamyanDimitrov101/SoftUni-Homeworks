namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    public class AquariumsTests
    {
        [Test]
        public void InitializeCorrectly()
        {
            string nameExpected = "ime";
            int capacityExpected = 10;
            Aquarium aquarium = new Aquarium("ime", 10);

            Assert.AreEqual(nameExpected, aquarium.Name);
            Assert.AreEqual(capacityExpected, aquarium.Capacity);
            Assert.AreEqual(0,aquarium.Count);
        }

        [Test]
        public void NameProp_WithNull_ThrowException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new Aquarium(null, 10));
        }
        [Test]
        public void NameProp_Correct()
        {
            var nameExpected = "ime";
              var aqurium = new Aquarium("ime", 10);
            Assert.AreEqual(nameExpected, aqurium.Name);
        }

        [Test]
        public void CapacityProp_WithLessThanZerro_ThrowExc()
        {
            var ex = Assert.Throws<ArgumentException>(
                   () => new Aquarium("ime", -10));
        }

        [Test]
        public void CapacityProp_Correct()
        {
            var capacityExpected= 10;
            var aqurium = new Aquarium("ime", 10);
            Assert.AreEqual(capacityExpected, aqurium.Capacity);
        }

        [Test]
        public void AddFishAboveCapacity_ThrowExc()
        {
            var aqurium = new Aquarium("ime", 1);

            aqurium.Add(new Fish("riba"));

            var ex = Assert.Throws<InvalidOperationException>(
                () => aqurium.Add(new Fish("riba")));
        }

        [Test]
        public void AddFishCorrectly()
        {
            var aqurium = new Aquarium("ime", 1);

            aqurium.Add(new Fish("riba"));
            var countExpected = 1;

            Assert.AreEqual(countExpected,aqurium.Count);
        }

        [Test]
        public void RemoveFish_NonExisting_ThrowExc()
        {
            var aqurium = new Aquarium("ime", 1);

            aqurium.Add(new Fish("riba"));
            

            var ex = Assert.Throws<InvalidOperationException>(
                () => aqurium.RemoveFish("nova"));
        }

        [Test]
        public void RemoveFishCorrectly()
        {
            var aqurium = new Aquarium("ime", 1);

            aqurium.Add(new Fish("riba"));
            var countExpected = 0;
            aqurium.RemoveFish("riba");

            Assert.AreEqual(countExpected, aqurium.Count);
        }

        [Test]
        public void SellFish_NonExisting_ThrowExc()
        {
            var aqurium = new Aquarium("ime", 1);

            aqurium.Add(new Fish("riba"));


            var ex = Assert.Throws<InvalidOperationException>(
                () => aqurium.SellFish("nova"));
        }

        [Test]
        public void SellFishCorrectly()
        {
            var aqurium = new Aquarium("ime", 1);

            var fish = new Fish("riba");
            aqurium.Add(fish);

            bool avaliableExpected = false;

            aqurium.SellFish("riba");

            Assert.AreEqual(avaliableExpected, fish.Available);
        }

        [Test]
        public void SellFishCorrectlyReturn()
        {
            var aqurium = new Aquarium("ime", 1);

            var fish = new Fish("riba");
            aqurium.Add(fish);


            var fishToReturn = aqurium.SellFish("riba");

            Assert.AreEqual(fish, fishToReturn);
        }

        [Test]
        public void ReportFishCorrectly()
        {
            var aqurium = new Aquarium("ime", 1);

            aqurium.Add(new Fish("riba"));

            string[] report = aqurium.Report().Split();

            Assert.Contains("riba",report);
        }
    }
}
