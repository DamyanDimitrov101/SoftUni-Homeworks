using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using PlayersAndMonsters.Validators;
using System;

namespace PlayersAndMonsters.Models.Players
{
    public abstract class Player : IPlayer
    {
        protected Player(ICardRepository cardRepository,string username,int health)
        {
            this.CardRepository = cardRepository;
            this.Username = username;
            this.Health = health;
        }

        private string username;
        private int health;

        public ICardRepository CardRepository { get; }

        public string Username 
        { 
            get => this.username;
            private set 
            {
                Validator.ValidateStringIfNullOrEmpty(value, "Player's username cannot be null or an empty string.");
                this.username = value;
            }
        }

        public int Health 
        { 
            get => this.health;
            set 
            {
                Validator.ValidateIntIfLessThanZerro(value, "Player's health bonus cannot be less than zero.");
                this.health = value;
            }
        }

        public bool IsDead => this.Health <=0;

        public void TakeDamage(int damagePoints)
        {
            Validator.ValidateIntIfLessThanZerro(damagePoints, "Damage points cannot be less than zero.");


            this.Health = Math.Max(this.Health - damagePoints, 0);
        }
    }
}
