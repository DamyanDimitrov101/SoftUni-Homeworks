using NUnit.Framework;

namespace Tests
{
    using ExtendedDatabase;
    using System;

    public class ExtendedDatabaseTests
    {
        private ExtendedDatabase extendedDatabase;
        private Person gosho;
        private Person pesho;

        [SetUp]
        public void Setup()
        {
            gosho = new Person(41,"Gosho");
            pesho = new Person(18746,"Pesho");
        }

        [Test]
        public void CheckConstructorFlow()
        {
            Person[] arr = new Person[] { pesho, gosho };

            extendedDatabase = new ExtendedDatabase(arr);

            int expectedCount = 2;
            var actualCount = extendedDatabase.Count;

            Assert.AreEqual(expectedCount, actualCount,null,"Database is not initialized with the parameters.");
        }

        [Test]
        public void AddMethodWorksProperly_AndDoesNotAddMoreThanSixteenElements()
        {
            Person[] arr = new Person[] { pesho, gosho , new Person(448854456,"Thest"), new Person(4456, "Tehgst"), new Person(454456, "Tevbst"), new Person(445546, "Teuyst"), new Person(4420156, "Tezxst"), new Person(44545566, "Tejhst"), new Person(445876, "Tesmt"), new Person(445648, "Teiust"), new Person(441056, "Tqweest"), new Person(44598246, "Test"), new Person(445996, "Tesast"), new Person(44546, "Teffst"), new Person(4452136, "Teiufst"), new Person(478456, "Tnbest") };

            extendedDatabase = new ExtendedDatabase(arr);
            
            var ex = Assert
                .Throws<InvalidOperationException>
                (() => extendedDatabase.Add(new Person(4479956, "Tawest")));
            Assert.AreEqual("Array's capacity must be exactly 16 integers!",ex.Message);
        }

        [TestCase("Gosho")]
        public void ThrowExceptionIfPersonWithTheSameUserNameExist(string username)
        {
            extendedDatabase = new ExtendedDatabase();

            extendedDatabase.Add(new Person(44566,username));

            var ex = Assert.Throws<InvalidOperationException>
                (() => extendedDatabase.Add(new Person(46,username)));
            Assert.AreEqual("There is already user with this username!",ex.Message);
        }

        [TestCase(7777)]
        [TestCase(0)]
        [TestCase(-7777)]
        public void ThrowExeptionIfTheNewUserIsWithTheSameID(int id)
        {
            extendedDatabase = new ExtendedDatabase();

            extendedDatabase.Add(new Person(id,"Pesho"));

            var ex = Assert.Throws<InvalidOperationException>
                (() => extendedDatabase.Add(new Person(id, "Georgi")));
            Assert.AreEqual("There is already user with this Id!", ex.Message);
        }

        [Test]
        public void CheckIfItsRemovingFromEmptyCollection_RemoveMethod()
        {
            extendedDatabase = new ExtendedDatabase();

            var ex = Assert.Throws<InvalidOperationException>
                (() => extendedDatabase.Remove());
        }

        [Test]
        public void CheckIfRemoveMethodIsRemoving_RemoveMethodCountDecrease()
        {
            extendedDatabase = new ExtendedDatabase(new Person(7777, "Pesho"));

            int expectedCount = 0;
            extendedDatabase.Remove();
            int currentCount = extendedDatabase.Count;

            Assert.AreEqual(expectedCount, currentCount);
        }

        [Test]
        public void TestFindByUsernameMethod_WithNull()
        {
            extendedDatabase = new ExtendedDatabase(new Person(7777, "Pesho"));

            var ex = Assert.Throws<ArgumentNullException>
                (() => extendedDatabase.FindByUsername(null));
            Assert.AreEqual("Username parameter is null!", ex.ParamName);
        }

        [TestCase("Pe")]
        public void TestFindByUsernameMethod_WithNonExistentUsername(string usernameToFind)
        {
            extendedDatabase = new ExtendedDatabase(new Person(7777, "Pesho"));

            var ex = Assert.Throws<InvalidOperationException>
                (() => extendedDatabase.FindByUsername(usernameToFind));
            Assert.AreEqual("No user is present by this username!", ex.Message);
        }

        [TestCase("Pesho")]
        public void ReturnsPersonWhenFindByUsernameMethodIsUsed(string usernameToFind)
        {
            extendedDatabase = new ExtendedDatabase(new Person(7777, "Pesho"));

            var expectedPersonName = "Pesho";
            var actualPersonName = extendedDatabase.FindByUsername(usernameToFind).UserName;

            Assert.AreEqual(expectedPersonName,actualPersonName);
        }

        [Test]
        public void TestFindByIdMethod_WithLessThanZeroNumber()
        {
            extendedDatabase = new ExtendedDatabase(new Person(7777, "Pesho"));

            var ex = Assert.Throws<ArgumentOutOfRangeException>
                (() => extendedDatabase.FindById(-7777));
            Assert.AreEqual("Id should be a positive number!", ex.ParamName);
        }

        [TestCase(46545)]
        public void TestFindByUsernameMethod_WithNonExistentId(int idToFind)
        {
            extendedDatabase = new ExtendedDatabase(new Person(7777, "Pesho"));

            var ex = Assert.Throws<InvalidOperationException>
                (() => extendedDatabase.FindById(idToFind));
            Assert.AreEqual("No user is present by this ID!", ex.Message);
        }

        [TestCase(7777)]
        public void ReturnsPersonWhenFindByIdMethodIsUsed(int idToFind)
        {
            extendedDatabase = new ExtendedDatabase(new Person(7777, "Pesho"));

            var expectedPersonId = 7777;
            var actualPersonId = extendedDatabase.FindById(idToFind).Id;

            Assert.AreEqual(expectedPersonId, actualPersonId);
        }

    }
}