using NUnit.Framework;

namespace Tests
{
    using FightingArena;
    using System;

    public class ArenaTests
    {
        private Arena arena;
        
        [SetUp]
        public void Setup()
        {
            arena = new Arena();
        }

        [Test]
        public void ConstructorInitializeCorrectly()
        {
            Assert.IsNotNull(arena);
        }

        [Test]
        public void TestEnrollMethodCorrectly()
        {
            Warrior warrior = new Warrior("Pesho",10,100);

            arena.Enroll(warrior);

            CollectionAssert.IsNotEmpty(arena.Warriors);
        }

        [Test]
        public void TestEnrollMethodWithExistingWarriorInTheArena_WithSameName()
        {
            Warrior warrior = new Warrior("Pesho",10,100);

            arena.Enroll(warrior);

            var ex = Assert.Throws<InvalidOperationException>
                (() => arena.Enroll(new Warrior("Pesho",45,800)));
            Assert.AreEqual("Warrior is already enrolled for the fights!", ex.Message);
        }

        [Test]
        public void TestEnrollMethodWithExistingWarriorInTheArena_WithSameDamage()
        {
            Warrior warrior = new Warrior("Pesho", 10, 100);

            arena.Enroll(warrior);

            Assert.DoesNotThrow(() => arena.Enroll(new Warrior("Tesho", 10, 800)));
        }
        [Test]
        public void TestEnrollMethodWithExistingWarriorInTheArena_WithSameHp()
        {
            Warrior warrior = new Warrior("Pesho", 10, 100);

            arena.Enroll(warrior);

            Assert.DoesNotThrow(() => arena.Enroll(new Warrior("Tesho", 70, 100)));
        }

        [Test]
        public void TestFightMethodCorrectly()
        {
            Warrior warrior1 = new Warrior("Pesho", 60, 500);
            Warrior warrior2 = new Warrior("Lesho", 50, 400);

            arena.Enroll(warrior1);
            arena.Enroll(warrior2);

            Assert.DoesNotThrow(() => arena.Fight(warrior1.Name, warrior2.Name));

            Assert.DoesNotThrow(()=>warrior1.Attack(warrior2));
        }

        [Test]
        public void TestFightMethod_WithNotEnrolledWarriorAttacker()
        {
            Warrior warrior1 = new Warrior("Pesho", 60, 500);
            Warrior warrior2 = new Warrior("Lesho", 40, 400);

            arena.Enroll(warrior2);

            var ex = Assert.Throws<InvalidOperationException>
                 (() => arena.Fight(warrior1.Name, warrior2.Name));
            Assert.AreEqual($"There is no fighter with name {warrior1.Name} enrolled for the fights!",ex.Message);
        }

        [Test]
        public void TestFightMethod_WithNotEnrolledWarriorDefender()
        {
            Warrior warrior1 = new Warrior("Pesho", 60, 500);
            Warrior warrior2 = new Warrior("Lesho", 40, 400);

            arena.Enroll(warrior1);

            var ex = Assert.Throws<InvalidOperationException>
                 (() => arena.Fight(warrior1.Name, warrior2.Name));
            Assert.AreEqual($"There is no fighter with name {warrior2.Name} enrolled for the fights!", ex.Message);
        }
    }
}
