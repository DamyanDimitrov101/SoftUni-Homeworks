using PlayersAndMonsters.Models.Players.Contracts;

namespace PlayersAndMonsters.Models.Players
{
    public class Advanced : Player, IPlayer
    {
        private const int advancedHealthPoints = 250;
        public Advanced(Repositories.Contracts.ICardRepository cardRepository, string username)
            : base(cardRepository, username, advancedHealthPoints)
        {
        }
    }
}
