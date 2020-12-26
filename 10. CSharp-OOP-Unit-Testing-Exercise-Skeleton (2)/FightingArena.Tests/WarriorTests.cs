using NUnit.Framework;

namespace Tests
{
    using FightingArena;
    using System;

    public class WarriorTests
    {
        private Warrior warrior;
        [SetUp]
        public void Setup()
        {
            warrior = new Warrior("Pesho", 10, 100);
        }

        [TestCase]
        public void TestConstructorInitializeCorrectly()
        {
            Assert.IsNotNull(this.warrior);
        }

        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void TestIfNameSetterThrowsException_WithNullOrWhiteSpaceOREmpty(string nameToSet)
        {
            var ex = Assert.Throws<ArgumentException>
                (() => new Warrior(nameToSet, 10, 100));
            Assert.AreEqual("Name should not be empty or whitespace!",ex.Message);
        }

        [TestCase(0)]
        [TestCase(-5)]
        public void TestIfDamageSetterThrowsException_WithLessThanZeroOrZero(int damageToSet)
        {
            var ex = Assert.Throws<ArgumentException>
                (() => new Warrior("Pesho", damageToSet, 100));
            Assert.AreEqual("Damage value should be positive!", ex.Message);
        }

        [TestCase(-5)]
        public void TestIfHPSetterThrowsException_WithLessThanZero(int hPToSet)
        {
            var ex = Assert.Throws<ArgumentException>
                (() => new Warrior("Pesho", 10, hPToSet));
            Assert.AreEqual("HP should not be negative!", ex.Message);
        }

        [Test]
        public void TestAttackMethod_WithNormalFlow()
        {
            Warrior warriorToAttrack = new Warrior("Tosho", 10, 100);

            int hpThisWariorExpected = 90;
            int hpToAttackWariorExpected = 90;
            this.warrior.Attack(warriorToAttrack);

            Assert.AreEqual(hpThisWariorExpected,this.warrior.HP);
            Assert.AreEqual(hpToAttackWariorExpected,warriorToAttrack.HP);
        }

        [Test]
        public void TestAttackMethod_WithNormalFlowWhenDamageIsMoreThanHp()
        {
            Warrior warriorToAttrack = new Warrior("Tosho", 10, 40);

            this.warrior = new Warrior("Pesho", 50, 120);

            this.warrior.Attack(warriorToAttrack);

            Assert.AreEqual(0, warriorToAttrack.HP);
        }

        [TestCase(1)]
        [TestCase(30)]
        public void AttackWithLessHPThanTheRequaredHp_AttackMethod(int hp)
        {
            Warrior warriorToAttrack = new Warrior("Tosho", 10, 100);
            warrior = new Warrior("Gosho", 10, hp);

            var ex = Assert.Throws<InvalidOperationException>
                (() => warrior.Attack(warriorToAttrack));
            Assert.AreEqual("Your HP is too low in order to attack other warriors!",ex.Message);
        }

        [TestCase(1)]
        [TestCase(30)]
        public void AttackWarriorToAttackHavingLessHPThanTheRequaredHP_AttackMethod(int hp)
        {
            Warrior warriorToAttrack = new Warrior("Tosho", 10, hp);

            const int MIN_ATTACK_HP = 30;


            var ex = Assert.Throws<InvalidOperationException>
                (() => warrior.Attack(warriorToAttrack));
            Assert.AreEqual($"Enemy HP must be greater than {MIN_ATTACK_HP} in order to attack him!", ex.Message);
        }

        [TestCase(50)]
        [TestCase(33)]
        public void ThrowExceptionIfThisWarriorHpIsLessThanEnemyDamage(int hp)
        {
            Warrior warriorToAttrack = new Warrior("Tosho", 100, 100);

            warrior = new Warrior("Mosho", 10, hp);


            var ex = Assert.Throws<InvalidOperationException>
                (() => warrior.Attack(warriorToAttrack));
            Assert.AreEqual("You are trying to attack too strong enemy", ex.Message);
        }
    }
}