namespace INStock.Tests
{
    using INStock.Contracts;
    using INStock.Models;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ProductTests
    {
        private IProduct productToTest;

        [SetUp]
        public void SetUp()
        {
            productToTest = new Product("Jack", 40, 9);
        }

        [TestCase("Jack",40,9)]
        [TestCase("",40,9)]
        [TestCase("Jack", 0, 9)]
        [TestCase("Jack", 40, 0)]

        public void InitializeProductProperlyTest(string label,decimal price, int quantity)
        {
            const string expectedLabel = "Jack";
            const decimal expectedPrice = 40m;
            const int expectedQuantity = 9;

            System.Console.WriteLine(productToTest.Price);

            Assert.AreEqual(expectedLabel,productToTest.Label);
            Assert.That(expectedPrice == productToTest.Price,"Price not initialized properly!");
            Assert.That(expectedQuantity==productToTest.Quantity,"Quantity not initialized properly!");
        }

        [Test]
        public void ThrowArguementExceptionIfInitializedWithNullForLaber()
        {
            Assert.Throws<ArgumentNullException>(() => new Product(null, 40, 9));
        }

        [Test]
        public void ThrowArguementExceptionIfInitializedWithNumBellowZeroForPrice()
        {
            Assert.Throws<IndexOutOfRangeException>(() => new Product("Jack", -4, 9));
        }

        [Test]
        public void ThrowArguementExceptionIfInitializedWithNumBellowZeroForQuantity()
        {
            Assert.Throws<IndexOutOfRangeException>(() => new Product("Jack", 40, -9));
        }

        [TestCase("JimBeam", 40, 9)]
        [TestCase("Jonny Walker", 4, 9)]
        public void CheckIfTwoProductsAreTheSame(string label, decimal price, int quantity)
        {
            IProduct productToComp = new Product(label, price, quantity);

            Assert.That(productToTest.CompareTo(productToComp)!=0);
        }
    }
}