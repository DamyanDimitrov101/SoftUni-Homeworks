using PlayersAndMonsters.Core.Factories.Contracts;
using PlayersAndMonsters.Models.Cards.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace PlayersAndMonsters.Core.Factories
{
    public class CardFactory : ICardFactory
    {
        public ICard CreateCard(string type, string name)
        {
            Type cardType = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(t => t.Name.StartsWith(type));

            if (cardType == null)
            {
                throw new ArgumentException("Card of this type does not exists!");
            }

            var instanceCard = Activator.CreateInstance(cardType, name) as ICard;

            return instanceCard;
        }
    }
}
