using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayersAndMonsters.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private Dictionary<string, IPlayer> players;

        public PlayerRepository()
        {
            this.players = new Dictionary<string, IPlayer>();
        }

        public int Count => this.players.Values.Count;

        public IReadOnlyCollection<IPlayer> Players => this.players.Values.ToList();

        public void Add(IPlayer player)
        {
            CheckIfPlayerIsNullAndThrowExc(player);

            if (this.players.ContainsKey(player.Username))
            {
                throw new ArgumentException($"Player {player.Username} already exists!");
            }

            this.players.Add(player.Username, player);
        }

        
        public IPlayer Find(string username)
        {
            return this.players.FirstOrDefault(p => p.Key == username).Value;
        }

        public bool Remove(IPlayer player)
        {
            CheckIfPlayerIsNullAndThrowExc(player);

            return this.players.Remove(player.Username);
        }

        private static void CheckIfPlayerIsNullAndThrowExc(IPlayer player)
        {
            if (player is null)
            {
                throw new ArgumentException("Player cannot be null");
            }
        }

    }
}
