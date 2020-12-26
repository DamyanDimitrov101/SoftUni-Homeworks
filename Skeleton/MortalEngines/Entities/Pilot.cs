using MortalEngines.Entities.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public class Pilot : IPilot
    {
        private IList machines;
        public Pilot(string name)
        {
            if (string.IsNullOrEmpty(name)||string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Pilot name cannot be null or empty string.");
            }
            this.Name = name;
            this.machines = new List<IMachine>();
        }
        public string Name { get; }

        public void AddMachine(IMachine machine)
        {
            if (machine == null)
            {
                throw new NullReferenceException("Null machine cannot be added to the pilot.");
            }
            machine.Pilot = this;
            this.machines.Add(machine);
        }



        public string Report()
        {
            return $"{this.Name} - {this.machines.Count} machines" +System.Environment.NewLine + string.Join(System.Environment.NewLine,this.machines);
        }
    }
}
