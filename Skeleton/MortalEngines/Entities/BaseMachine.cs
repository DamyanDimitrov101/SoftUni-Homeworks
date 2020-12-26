using MortalEngines.Entities.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public abstract class BaseMachine : IMachine
    {
        private IPilot pilot;
        protected double attackPoints;
        protected double defensePoints;
        protected double healthPoints;

        public BaseMachine(string name,double attackPoints,double defensePoints, double healthPoints)
        {
            if (string.IsNullOrEmpty(name)||string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(Name),"Machine name cannot be null or empty.");
            }
            this.Name = name;
            if (healthPoints<0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.HealthPoints = healthPoints;
            if (attackPoints< 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.attackPoints = attackPoints;
            if (defensePoints < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.defensePoints= defensePoints;

            this.Targets = new List<string>();
        }

        public string Name { get; }

        public IPilot Pilot 
        { 
            get => this.pilot; 
            set 
            {
                if (value == null)
                {
                    throw new NullReferenceException("Pilot cannot be null.");
                }
                this.pilot = value;
            } 
        }
        public double HealthPoints { get => this.healthPoints; set => this.healthPoints = value; }

        public double AttackPoints { get => this.attackPoints; }

        public double DefensePoints { get => this.defensePoints; }

        public IList<string> Targets { get; }

        public void Attack(IMachine target)
        {
            if (target == null)
            {
                throw new NullReferenceException("Target cannot be null");
            }
            if (target.HealthPoints<=0)
            {
                throw new ArgumentException("Target HP is 0");
            }

            // Main Attack Logic
            if (target.DefensePoints < this.AttackPoints)
            {
                target.HealthPoints -=Math.Abs(target.DefensePoints - this.AttackPoints);
            }
            if (target.HealthPoints<0)
            {
                target.HealthPoints = 0;
            }
            this.Targets.Add(target.Name);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"- {this.Name}");
            sb.AppendLine($" *Type: {this.GetType().Name}");
            sb.AppendLine($" *Health: {this.HealthPoints:F2}");
            sb.AppendLine($" *Attack: {this.AttackPoints:F2}");
            sb.AppendLine($" *Defense: {this.DefensePoints:F2}");
            if (Targets.Count==0)
            {
                sb.Append(" *Targets: " + "None");
            }
            else
            {
                sb.Append(" *Targets: "+string.Join(",",this.Targets));
            }
            return sb.ToString();
        }
    }
}
