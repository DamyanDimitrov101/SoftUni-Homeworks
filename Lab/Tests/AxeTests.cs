using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AxeTests
    {
        [TestCase(10, 10, 10, 10)]
        [TestCase(11,20,10,10)]
        [TestCase(10,5,10,10)]
        public void LoseDurability_WeaponShouldLoseItsDurabilityAfterEachAttack
            (
             int axeAttack
            ,int axeDurability
            ,int dummyHealth
            ,int dummyExpirience
            )
        {
            // Arrange

            Axe axe = new Axe(axeAttack, axeDurability);
            Dummy dummy = new Dummy(dummyHealth, dummyExpirience);

            var durabilityCurrent = axe.DurabilityPoints;

            // Act

            axe.Attack(dummy);

            // Assure

            Assert.That(axe.DurabilityPoints,Is.LessThan(durabilityCurrent));
        }
    }
}