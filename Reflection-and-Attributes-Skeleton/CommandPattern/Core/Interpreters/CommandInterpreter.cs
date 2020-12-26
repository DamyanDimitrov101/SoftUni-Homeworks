using CommandPattern.Core.Commands;
using CommandPattern.Core.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace CommandPattern.Interpeters
{
    internal class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] inputArr = args.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string commandName = (inputArr[0]+"Command").ToLower();
            
            string[] commandArgs = inputArr.Skip(1).ToArray();

            Type commandType = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name.ToLower() == commandName);

           // Type commandType = Type.GetType($"CommandPattern.Core.Commands.{commandName}",false,true);
           
            if (commandType==null)
            {
                throw new ArgumentException("Invalid command type!");
            }

            ICommand instance = Activator.CreateInstance(commandType) as ICommand;

            if (instance == null)
            {
                throw new ArgumentException("Invalid command type!");
            }

            string result = instance.Execute(commandArgs);

            return result;
        }
    }
}