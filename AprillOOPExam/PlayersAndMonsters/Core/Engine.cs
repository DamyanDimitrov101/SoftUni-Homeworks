using PlayersAndMonsters.Core.Contracts;
using PlayersAndMonsters.Core.Factories;
using PlayersAndMonsters.Core.Factories.Contracts;
using PlayersAndMonsters.IO;
using PlayersAndMonsters.IO.Contracts;
using PlayersAndMonsters.Models.BattleFields;
using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Repositories;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new FileWriter();

            IPlayerFactory playerFactory = new PlayerFactory();
            ICardFactory cardFactory = new CardFactory();

            IPlayerRepository playerRepository = new PlayerRepository();
            ICardRepository cardRepository = new CardRepository();

            IBattleField battleField = new BattleField();

            IManagerController managerController = new ManagerController
                (playerFactory
                ,cardFactory
                ,cardRepository
                ,playerRepository
                ,battleField);

            ICommandInterpreter commandInterpreter = new CommandInterpreter(managerController);

            var sb = new StringBuilder();

            while (true)
            {
                string[] inputArgs = reader.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                if (inputArgs[0]=="Exit")
                {
                    break;
                }

                try
                {
                    string result = commandInterpreter.Read(inputArgs);
                    writer.Write(result);
                    sb.AppendLine(result);                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    sb.AppendLine(ex.InnerException.Message);
                }
            }
        }
    }
}
