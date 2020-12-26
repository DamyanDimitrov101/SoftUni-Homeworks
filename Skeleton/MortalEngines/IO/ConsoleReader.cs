using MortalEngines.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MortalEngines.IO
{
    public class ConsoleReader : IReader
    {
        public IList<ICommand> ReadCommands()
        {
            List<ICommand> commands = new List<ICommand>();
            string inputRow = Console.ReadLine();
            while (inputRow!="Quit")
            {
                string[] arr = inputRow.Split(" ",StringSplitOptions.RemoveEmptyEntries);

                string name = arr[0];
                string[] parameters = arr.Skip(1).ToArray();

                Command command = new Command(name,parameters);
                commands.Add(command);
                inputRow = Console.ReadLine();
            }
            return commands;
        }
    }
}
