namespace PlayersAndMonsters.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Contracts;
    using PlayersAndMonsters.Common;
    using PlayersAndMonsters.Core.Factories.Contracts;
    using PlayersAndMonsters.Models.BattleFields;
    using PlayersAndMonsters.Models.BattleFields.Contracts;
    using PlayersAndMonsters.Models.Cards.Contracts;
    using PlayersAndMonsters.Models.Players.Contracts;
    using PlayersAndMonsters.Repositories.Contracts;

    public class ManagerController : IManagerController
    {
        private IPlayerFactory playerFactory;
        private ICardFactory cardFactory;

        private ICardRepository cardRepository;
        private IPlayerRepository playerRepository;

        private IBattleField battleField;

        public ManagerController
            (IPlayerFactory playerFactory
            , ICardFactory cardFactory
            , ICardRepository cardRepository
            , IPlayerRepository playerRepository
            , IBattleField battleField)
        {
            this.playerFactory = playerFactory;
            this.cardFactory = cardFactory;
            this.cardRepository = cardRepository;
            this.playerRepository = playerRepository;

            this.battleField = battleField;
        }

        public string AddPlayer(string type, string username)
        {
            var instance = this.playerFactory.CreatePlayer(type, username);

                playerRepository.Add(instance);

            return string.Format(ConstantMessages.SuccessfullyAddedPlayer, type, username);
        }

        public string AddCard(string type, string name)
        {
            var instance = this.cardFactory.CreateCard(type,name);

            cardRepository.Add(instance);

            return string.Format(ConstantMessages.SuccessfullyAddedCard,type,name);
        }

        public string AddPlayerCard(string username, string cardName)
        {
            var playerInstance = this.playerRepository.Find(username);

            if (playerInstance is null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            var cardInstance = this.cardRepository.Find(cardName);

            if (cardInstance is null)
            {
                throw new ArgumentException("Card cannot be null!");
            }

            playerInstance.CardRepository.Add(cardInstance);

            return string.Format(ConstantMessages.SuccessfullyAddedPlayerWithCards, cardName, username);
        }

        public string Fight(string attackUser, string enemyUser)
        {
            var attacker = this.playerRepository.Find(attackUser);
            var defender = this.playerRepository.Find(enemyUser);

            if (attacker is null || defender is null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            battleField.Fight(attacker, defender);

            return string.Format(ConstantMessages.FightInfo,attacker.Health,defender.Health);
        }

        public string Report()
        {
            var sb = new StringBuilder();

            foreach (var player in this.playerRepository.Players)
            {
                sb.AppendLine(string.Format(ConstantMessages.PlayerReportInfo
                    ,player.Username
                    ,player.Health
                    ,player.CardRepository.Count));

                foreach (var card in player.CardRepository.Cards)
                {
                    sb.AppendLine(string.Format(ConstantMessages.CardReportInfo
                        ,card.Name
                        ,card.DamagePoints));
                }
                sb.AppendLine(ConstantMessages.DefaultReportSeparator);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
