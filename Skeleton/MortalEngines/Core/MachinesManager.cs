namespace MortalEngines.Core
{
    using Contracts;
    using MortalEngines.Common;
    using MortalEngines.Entities;
    using MortalEngines.Entities.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MachinesManager : IMachinesManager
    {
        private List<IPilot> pilots;
        private List<IMachine> machines;

        public MachinesManager()
        {
            this.pilots = new List<IPilot>();
            this.machines = new List<IMachine>();
        }

        public string HirePilot(string name)
        {
            Pilot pilot = new Pilot(name);
            if (pilots.Contains(pilot))
            {
                return string.Format(OutputMessages.PilotExists,name);
            }
            pilots.Add(pilot);
            return string.Format(OutputMessages.PilotHired,name);
        }

        public string ManufactureTank(string name, double attackPoints, double defensePoints)
        {
            Tank tank = new Tank(name,attackPoints,defensePoints);
            if (this.machines.Contains(tank))
            {
                return string.Format(OutputMessages.MachineExists,name);
            }

            this.machines.Add(tank);
            return string.Format(OutputMessages.TankManufactured,tank.Name,tank.AttackPoints,tank.DefensePoints);
        }

        public string ManufactureFighter(string name, double attackPoints, double defensePoints)
        {
            Fighter fighter= new Fighter(name, attackPoints, defensePoints);
            if (this.machines.Contains(fighter))
            {
                return string.Format(OutputMessages.MachineExists, name);
            }

            this.machines.Add(fighter);
            return string.Format(OutputMessages.FighterManufactured,fighter.Name,fighter.AttackPoints,fighter.DefensePoints,(fighter.AggressiveMode==true?"ON":"OFF"));
        }

        public string EngageMachine(string selectedPilotName, string selectedMachineName)
        {
            var pilot = this.pilots.FirstOrDefault(p => p.Name == selectedPilotName);
            var machine = this.machines.FirstOrDefault(m=>m.Name==selectedMachineName);

            if (pilot is Pilot pilot1&&machine is IMachine machine1)
            {
                if (!this.pilots.Contains(pilot1))
                {
                    return string.Format(OutputMessages.PilotNotFound,selectedPilotName);
                }
                if (!this.machines.Contains(machine1))
                {
                    return string.Format(OutputMessages.MachineNotFound,selectedMachineName);
                }

                if (machine1.Pilot!=null)
                {
                    return string.Format(OutputMessages.MachineHasPilotAlready,selectedMachineName);
                }
                if (pilot1.Report().Contains(selectedMachineName))
                {
                    return string.Format(OutputMessages.MachineHasPilotAlready,selectedMachineName);
                }

                pilot1.AddMachine(machine1);
                return string.Format(OutputMessages.MachineEngaged, selectedPilotName, selectedMachineName);
            }
            return string.Format(OutputMessages.PilotNotFound, selectedPilotName);
        }

        public string AttackMachines(string attackingMachineName, string defendingMachineName)
        {
            var machineAttacker = this.machines.FirstOrDefault(m=>m.Name==attackingMachineName);
            var machineDefender = this.machines.FirstOrDefault(m => m.Name == defendingMachineName);


            if (machineAttacker is IMachine attacker&&this.machines.Contains(machineAttacker))
            {
                if (machineDefender is IMachine defender&&this.machines.Contains(machineDefender))
                {
                    if (attacker.HealthPoints <= 0)
                    {
                        return string.Format(OutputMessages.DeadMachineCannotAttack,attackingMachineName);
                    }
                    if (defender.HealthPoints<=0)
                    {
                        return string.Format(OutputMessages.DeadMachineCannotAttack, defendingMachineName);
                    }

                    attacker.Attack(defender);

                    return string.Format(OutputMessages.AttackSuccessful, defendingMachineName, attackingMachineName, defender.HealthPoints);
                }
                return string.Format(OutputMessages.MachineNotFound,defendingMachineName);
            }
            return string.Format(OutputMessages.MachineNotFound,attackingMachineName);
        }

        public string PilotReport(string pilotReporting)
        {
            var pilot = this.pilots.FirstOrDefault(p => p.Name == pilotReporting);

            if (pilot!=null)
            {
                return pilot.Report();
            }
            return string.Format(OutputMessages.PilotNotFound, pilotReporting);

        }

        public string MachineReport(string machineName)
        {
            var machine = this.machines.FirstOrDefault(m => m.Name ==machineName);

            if (machine != null)
            {
                return machine.ToString();
            }
            return string.Format(OutputMessages.MachineNotFound,machineName);
        }

        public string ToggleFighterAggressiveMode(string fighterName)
        {
            var fighter = this.machines.FirstOrDefault(f => f.Name == fighterName);
            if (fighter is Fighter fighter1)
            {
                fighter1.ToggleAggressiveMode();
                return string.Format(OutputMessages.FighterOperationSuccessful,fighterName);
            }
            return string.Format(OutputMessages.MachineNotFound,fighterName);
        }

        public string ToggleTankDefenseMode(string tankName)
        {
            var tank = this.machines.FirstOrDefault(t => t.Name == tankName);
            if (tank is Tank tank1)
            {
                tank1.ToggleDefenseMode();
                return string.Format(OutputMessages.TankOperationSuccessful, tankName);
            }
            return string.Format(OutputMessages.MachineNotFound,tankName);
        }
    }
}