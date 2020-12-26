using MortalEngines.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.IO
{
    public class Command : ICommand
    {
        public Command(string name, string[] parameters)
        {
            Name = name;
            Parameters = parameters;
        }

        public string Name { get ; set ; }
        public string[] Parameters { get ; set ; }
    }
}
