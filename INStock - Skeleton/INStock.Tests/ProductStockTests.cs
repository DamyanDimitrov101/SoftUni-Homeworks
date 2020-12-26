namespace INStock.Tests
{
    using INStock.Contracts;
    using INStock.Models;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ProductStockTests
    {
        private static IProductStock pStock;
        [SetUp]
        public static void SetUp()
        {
             pStock = new ProductStock(); 
        }

        [Test]
        public void TestInitialization()
        {
            Assert.DoesNotThrow(()=>pStock.Add(new Product("Jack",3,4)));
        }

        [Test]
        public void TestCountMethod()
        {
            Assert.That(pStock.Count == 0);

        }

        [Test]
        public void AddMethodTest()
        {
            pStock.Add(new Product("Jack", 40, 9));
            Assert.That(pStock.Count == 1);
        }

        [Test]
        public void ContainsMethodTest()
        {
            Product product = new Product("Jack", 40, 9);
            
            pStock.Add(product);
            Assert.That(pStock.Contains(product));
            Assert.That(pStock.Count == 1);
        }

        [Test]
        public void FindMethodRethurnsCorrectly()
        {
            Product product = new Product("Jack", 40, 9);
            pStock.Add(product);

            IProduct actual = pStock.Find(0);

            Assert.That(product, Is.EqualTo(actual));
        }

        [TestCase(-1)]
        [TestCase(1)]
        public void FindMehodShouldThrowIndexOutOfRangeExc_WhenWrongIndexIsGiven(int index)
        {
            Product product = new Product("Jack", 40, 9);
            pStock.Add(product);

            Assert.Throws<IndexOutOfRangeException>(() => pStock.Find(index));
        }

        [Test]
        public void FindAllByPriceMethodRethurnsCorrectly()
        {
            Product product1 = new Product("Jack", 40, 9);
            Product product2 = new Product("JimBeam", 40, 91);
            pStock.Add(product1);
            pStock.Add(product2);

            IEnumerable<IProduct> actual = pStock.FindAllByPrice(40);

            Assert.That(pStock.Count,Is.EqualTo(2));
            Assert.That(pStock.Find(0), Is.EqualTo(product1));
            Assert.That(pStock.Find(1), Is.EqualTo(product2));
        }

        [TestCase(-1)]
        [TestCase(10)]
        public void FindAllByPriceMehodShouldThrowInvalidOperationExc_WhenWrongPriceIsGiven(int price)
        {
            Product product = new Product("Jack", 40, 9);
            pStock.Add(product);

            Assert.Throws<InvalidOperationException>(() => pStock.FindAllByPrice(price));
        }

        [Test]
        public void FindAllByQuantityMethodRethurnsCorrectly()
        {
            int quantity = 9;
            Product product1 = new Product("Jack", 240, quantity);
            Product product2 = new Product("JimBeam", 140, quantity);
            pStock.Add(product1);
            pStock.Add(product2);

            IEnumerable<IProduct> actual = pStock.FindAllByQuantity(quantity);

            Assert.That(pStock.Count, Is.EqualTo(2));
            Assert.That(pStock.Find(0), Is.EqualTo(product1));
            Assert.That(pStock.Find(1), Is.EqualTo(product2));
        }

        [TestCase(-1)]
        [TestCase(10)]
        public void FindAllByQuantityMehodShouldThrowInvalidOperationExc_WhenWrongPriceIsGiven(int quantity)
        {
            Product product = new Product("Jack", 40, 9);
            pStock.Add(product);

            Assert.Throws<InvalidOperationException>(() => pStock.FindAllByQuantity(quantity));
        }

        [Test]
        public void FindAllInRangeMethodRethurnsCorrectly()
        {
            Product product1 = new Product("Jack", 12, 40);
            Product product2 = new Product("JimBeam", 8, 120);
            pStock.Add(product1);
            pStock.Add(product2);

            List<IProduct> expectedColl = new List<IProduct>();
            expectedColl.Add(product2);
            expectedColl.Add(product1);

            IEnumerable<IProduct> actual = pStock.FindAllInRange(8,12);
            
            Assert.That(pStock.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(expectedColl, actual);
        }

        [TestCase(1,5)]
        public void FindAllInRangeMehodShouldThrowInvalidOperationExc_WhenWrongRangeIsGiven(double lo,double hi)
        {
            Product product = new Product("Jack", 40, 9);
            pStock.Add(product);

            Assert.Throws<InvalidOperationException>(() => pStock.FindAllInRange(lo,hi));
        }

        [Test]
        public void FindByLabelMethodRethurnsCorrectly()
        {
            Product product1 = new Product("Jack", 12, 40);
            Product product2 = new Product("JimBeam", 8, 120);
            pStock.Add(product1);
            pStock.Add(product2);


            IProduct actual = pStock.FindByLabel("Jack");

            Assert.That(actual, Is.EqualTo(product1));
        }

        [TestCase("JimBeam")]
        [TestCase("Jonny Walker")]
        public void FindByLabelMehodShouldThrowInvalidOperationExc_WhenWrongLabelIsGiven(string label)
        {
            Product product = new Product("Jack", 40, 9);
            pStock.Add(product);

            Assert.Throws<InvalidOperationException>(() => pStock.FindByLabel(label));
        }

        [Test]
        public void FindMostExpensiveProductMethodRethurnsCorrectly()
        {
            Product product1 = new Product("Savoy", 13, 120);
            Product product2 = new Product("Jack", 42, 40);
            Product product3 = new Product("JimBeam", 28, 120);


            pStock.Add(product1);
            pStock.Add(product2);
            pStock.Add(product3);

            IProduct actual = pStock.FindMostExpensiveProduct();

            Assert.That(actual, Is.EqualTo(product2));
        }

        [Test]
        public void FindMostExpensiveProductMehodShouldThrowInvalidOperationExc_WhenCollectionIsEmpty()
        {
            Product product = new Product("Jack", 40, 9);
            
            Assert.Throws<InvalidOperationException>(() => pStock.FindMostExpensiveProduct());
        }

        [Test]
        public void RemoveMethodIsSuccesfull()
        {
            Product product1 = new Product("Savoy", 13, 120);
            Product product2 = new Product("Jack", 42, 40);
            Product product3 = new Product("JimBeam", 28, 120);


            pStock.Add(product1);
            pStock.Add(product2);
            pStock.Add(product3);

            bool actual = pStock.Remove(product2);

            Assert.That(pStock.Count, Is.EqualTo(2));
            Assert.IsTrue(actual);
            Assert.That(pStock.Contains(product1));
            Assert.That(pStock.Contains(product3));
        }

        [Test]
        public void RemoveMethodShouldThrowInvalidOperationExceptionIfCollectionIsEmpty()
        {
            Product product = new Product("Jack", 40, 9);

            Assert.Throws<InvalidOperationException>(() => pStock.Remove(product));
        }

        [Test]
        public void RemoveMethodShouldReturnFalseIfNonExistingProductIsGiven()
        {
            Product product1 = new Product("Savoy", 13, 120);
            Product product2 = new Product("Jack", 42, 40);
            Product product3 = new Product("JimBeam", 28, 120);


            pStock.Add(product1);
            pStock.Add(product3);

            bool actual = pStock.Remove(product2);

            Assert.That(pStock.Count, Is.EqualTo(2));
            Assert.IsFalse(actual);
            Assert.That(pStock.Contains(product1));
            Assert.That(pStock.Contains(product3));
        }
    }
}
