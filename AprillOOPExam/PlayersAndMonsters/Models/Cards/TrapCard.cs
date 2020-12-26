using PlayersAndMonsters.Models.Cards.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Cards
{
    public class TrapCard : Card,ICard
    {
        private const int damagePointsTrapCard = 120;
        private const int healthPointsMagic = 5;
        public TrapCard(string name) : base(name, damagePointsTrapCard, healthPointsMagic)
        {
        }
    }
}
