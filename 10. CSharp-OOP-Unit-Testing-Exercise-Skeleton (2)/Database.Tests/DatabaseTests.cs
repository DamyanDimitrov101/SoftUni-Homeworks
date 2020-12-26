using NUnit.Framework;

namespace Tests
{
    using Database;
    using System;

    public class DatabaseTests
    {
        private Database database;
        private const int sizeOfCurrentDB = 16;

        [SetUp]
        public void Inititialize()
        {
            int[] arr = new int[sizeOfCurrentDB];
            this.database = new Database(arr);
        }

        [TestCase]
        public void CheckForStoringArrayCapacity_ShouldThrowExcIfNotEqualTo16()
        {
            int[] arr = new int[17];
            Assert.Throws<InvalidOperationException>(() => new Database(arr));
        }

        [TestCase]
        public void AddMethodShouldIncreaseTheCount()
        {
            database = new Database();
            database.Add(1);

            int expectedCount = 1;
            int actualCount = database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase]
        public void AddMethodShouldAddOnEmptyIndex()
        {
            int[] arr = new int[] { 1,2,3,4,5,6};

            database = new Database(arr);
            database.Add(7);

            int expectedResult = 7;
            int actialResult = database.Fetch()[6];

            Assert.AreEqual(expectedResult, actialResult,null,actialResult.ToString());
        }

        [TestCase]
        public void AddMethodExceedsTheCapacity_AddOperationShouldBeLessOrEqualToCapacityIfNotThrowsAnException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => database.Add(88));
            Assert.That(ex.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
        }

        [TestCase]
        public void CheckIfDatabaseIsNotEmptyWhenRemoveMethod()
        {
            database = new Database();
            var ex = Assert.Throws<InvalidOperationException>(() => database.Remove(),"Database count is 0.");
            Assert.That(ex.Message, Is.EqualTo("The collection is empty!"));
        }

        [Test]
        public void RemoveMethodMakesRemovesCorrectly()
        {
            int[] arr = { 1,2,3};
            database = new Database(arr);
            database.Remove();

            int expectedVelue = 2;
            int actualValue = database.Fetch()[database.Count-1];

            Assert.AreEqual(expectedVelue,actualValue);
        }

        [TestCase]
        public void FetchMethodShouldRethurnAllElementsAsArray()
        {
            int[] arr = {1,2,3,4,5 };
            database = new Database(arr);

            int[] expectedValue = { 1,2,3,4,5 };
            var actualValue = database.Fetch();

            CollectionAssert.AreEqual(expectedValue,actualValue);
        }
    }
}