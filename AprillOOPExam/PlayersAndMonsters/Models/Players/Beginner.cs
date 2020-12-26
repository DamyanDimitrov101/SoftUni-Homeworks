using PlayersAndMonsters.Models.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Players
{
    public class Beginner : Player,IPlayer
    {
        private const int beginnerInitialHealthPoints = 50;
        public Beginner(Repositories.Contracts.ICardRepository cardRepository, string username)
            : base(cardRepository, username, beginnerInitialHealthPoints)
        {
        }


    }
}
