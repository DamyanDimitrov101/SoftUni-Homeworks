using PlayersAndMonsters.Models.Cards.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayersAndMonsters.Repositories
{
    public class CardRepository : ICardRepository
    {
        private Dictionary<string, ICard> cards;

        public CardRepository()
        {
            this.cards = new Dictionary<string, ICard>();
        }

        public int Count => this.cards.Count;

        public IReadOnlyCollection<ICard> Cards => this.cards.Values.ToList();

        public void Add(ICard card)
        {
            CheckIfCardIsNullAndThrowExc(card);

            if (this.cards.ContainsKey(card.Name))
            {
                throw new ArgumentException($"Card {card.Name} already exists!");
            }

            this.cards.Add(card.Name,card);
        }


        public ICard Find(string name)
        {
            return this.cards.FirstOrDefault(c=>c.Key == name).Value;
        }

        public bool Remove(ICard card)
        {
            CheckIfCardIsNullAndThrowExc(card);

            return this.cards.Remove(card.Name);
        }


        private static void CheckIfCardIsNullAndThrowExc(ICard card)
        {
            if (card is null)
            {
                throw new ArgumentException("Card cannot be null!");
            }
        }
    }
}
