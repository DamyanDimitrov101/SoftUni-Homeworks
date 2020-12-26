using PlayersAndMonsters.Models.Cards.Contracts;
using PlayersAndMonsters.Validators;

namespace PlayersAndMonsters.Models.Cards
{
    public abstract class Card : ICard
    {
        private string name;
        private int damagePoints;
        private int healthPoints;

        protected Card(string name, int damagePoints, int healthPoints)
        {
            this.Name = name;
            this.DamagePoints = damagePoints;
            this.HealthPoints = healthPoints;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                Validator.ValidateStringIfNullOrEmpty(value, "Card's name cannot be null or an empty string.");
                this.name = value;
            }
        }

        public int DamagePoints 
        { 
            get => this.damagePoints;
            set 
            {
                Validator.ValidateIntIfLessThanZerro(value, "Card's damage points cannot be less than zero.");
                this.damagePoints = value;
            }
        }

        public int HealthPoints 
        { 
            get => this.healthPoints;
            private set 
            {
                Validator.ValidateIntIfLessThanZerro(value, "Card's HP cannot be less than zero.");
                this.healthPoints = value;
            }
        }
    }
}
