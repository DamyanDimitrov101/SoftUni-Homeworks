using PlayersAndMonsters.Models.Cards.Contracts;

namespace PlayersAndMonsters.Models.Cards
{
    public class MagicCard : Card, ICard
    {
        private const int damagePointsMagic = 5;
        private const int healthPointsMagic = 80;
        public MagicCard(string name) 
            : base(name, damagePointsMagic, healthPointsMagic)
        {
        }
    }
}
