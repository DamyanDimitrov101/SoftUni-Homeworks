using PlayersAndMonsters.Core.Factories.Contracts;
using PlayersAndMonsters.Models.Players;
using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PlayersAndMonsters.Core.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        public IPlayer CreatePlayer(string type, string username)
        {
            Type playerType = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(t=>t.Name==type);

            if (playerType==null)
            {
                throw new ArgumentException("Player of this type does not exists!");
            }

            var instance = Activator.CreateInstance(playerType, new CardRepository(),username) as IPlayer;

            //IPlayer player = null;
            //switch (type)
            //{
            //    case "Beginner":
            //        player = new Beginner(new CardRepository(),username);
            //        break;
            //    case "Advanced":
            //        player = new Advanced(new CardRepository(), username);
            //        break;
            //}

            return instance;
        }
    }
}
