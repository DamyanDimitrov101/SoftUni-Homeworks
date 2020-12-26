using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class DummyTests
    {
        private const int healthDummy = 10;
        private const int experienceDummy = 10;
        private Dummy dummy;

        [SetUp]
        public void Initialize()
        {
            this.dummy = new Dummy(healthDummy, experienceDummy);
        }

        [TestCase(10)]
        [TestCase(32)]
        public void DummyLosesHealth_WhenAttackedShouldLoseHealth
            (int attackPoints)
        {
            // Arrange
            var healthDummyCurrent = this.dummy.Health;
            // Act
            this.dummy.TakeAttack(attackPoints);
            // Assure
            Assert.That(dummy.Health, Is.LessThan(healthDummyCurrent),"The health of the Dummy doesnt change.");
        }


        [TestCase(15)]
        public void ThrowsException_IfDummyIsDeadThrowInvalidOperationException
            (int attackPoints)
        {
            Dummy dummy = new Dummy(10, 10);

            dummy.TakeAttack(attackPoints);

            var ex = Assert.Throws<InvalidOperationException>(()=>dummy.TakeAttack(attackPoints),"The attack doesnt throw exception.");

            Assert.That(ex.Message, Is.EqualTo("Dummy is dead."), "The attack doesnt throw the correct exception.");
        }

        [TestCase(12)]
        public void GiveXPIfDead_WhenDummyIsDeadItShouldGiveXP
            (int attackPoints)
        {
            while (!dummy.IsDead())
            {
                dummy.TakeAttack(attackPoints);
            }

            Assert.That(dummy.GiveExperience()==experienceDummy,"The experience of the dummy is not correct.");
        }

        [TestCase]
        public void CantGiveXP_WhenDummyIsAliveItShouldntGiveXP()
        {
            Exception ex = Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience(),"GiveExperiance method doesnt throw exception, dummy is dead.");
            Assert.That(ex.Message, Is.EqualTo("Target is not dead."),"The exception is not the correct one");
        }
    }
}
