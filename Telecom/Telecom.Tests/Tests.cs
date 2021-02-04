namespace Telecom.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using Telecom;
    [TestFixture]
    public class Tests
    {
        Phone phone;
        [SetUp]
        public void SetUp()
        {
            phone = new Phone("Samsung","Note8");
        }

        [Test]
        public void CheckContstructorInitializationProcess()
        {
            Assert.IsNotNull(phone);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("")]
        public void TestMakeSetter_WithNullOrEmpty_IfTrueThrowArguementException(string input)
        {
            var ex = Assert.Throws<ArgumentException>(()=>phone = new Phone(input, "Note8"));
            Assert.AreEqual($"Invalid Make!",ex.Message);
        }

        [Test]
        public void TestMakeSetterCorrectly()
        {
            string makeExpected = "Samsung";
            phone = new Phone("Samsung", "Note8");
            Assert.AreEqual(makeExpected, phone.Make);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("")]
        public void TestModelSetter_WithNullOrEmpty_IfTrueThrowArguementException(string input)
        {
            var ex = Assert.Throws<ArgumentException>(() => phone = new Phone("Samsung", input));
            Assert.AreEqual($"Invalid Model!", ex.Message);
        }

        [Test]
        public void TestModelSetterCorrectly()
        {
            string modelExpected = "Note8";
            phone = new Phone("Samsung", "Note8");
            Assert.AreEqual(modelExpected, phone.Model);
        }

        [Test]
        public void TestCountWhenInitialize()
        {
            int countExpected = 0;
            var countActual = this.phone.Count;

            Assert.AreEqual(countExpected, countActual);
        }

        [Test]
        public void TestAddContactMethod_WithExistingPersonName_IfTrueThrowException()
        {
            phone.AddContact("Pesho", "05521546");

            var ex = Assert.Throws<InvalidOperationException>(() => phone.AddContact("Pesho", "05521546484542"));
            Assert.AreEqual("Person already exists!",ex.Message);
        }

        [TestCase("Pesho")]
        [TestCase(" ")]
        public void TestIfAddContactAddsToThePhoneBook(string inputName)
        {
            int expectedCount = 1;
            phone.AddContact(inputName,"041532");

            Assert.AreEqual(expectedCount,phone.Count);
        }

        [Test]
        public void TestCallMethod_WithNonExistingPerson()
        {
            phone.AddContact("Pesho", "05521546");

            var ex = Assert.Throws<InvalidOperationException>(()=>phone.Call("Lesho"));

            Assert.AreEqual("Person doesn't exists!", ex.Message);
        }

        [TestCase("Gesho")]
        [TestCase("Vesho")]
        public void TestCallMethod_WithExistingPerson(string namePerson)
        {
            string phoneExpected = "05521546"+"...";
            phone.AddContact(namePerson, "05521546");
            string ToReturn = phone.Call(namePerson);
            string numberActual = ToReturn.Split(" - ").ToArray()[1];
            Assert.DoesNotThrow(() => phone.Call(namePerson));
            Assert.AreEqual(phoneExpected,numberActual);
            Assert.AreEqual($"Calling {namePerson} - {"05521546"}...", ToReturn);
        }
    }
}