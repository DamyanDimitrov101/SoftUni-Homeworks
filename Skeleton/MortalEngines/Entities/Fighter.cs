using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public class Fighter : BaseMachine,IFighter
    {
        private bool aggressiveMode = false;

        public Fighter(string name, double attackPoints, double defensePoints) : base(name, attackPoints, defensePoints,200)
        {
            this.ToggleAggressiveMode();
        }

        public bool AggressiveMode
        {
            get { return aggressiveMode; }
        }

        public void ToggleAggressiveMode()
        {
            if (this.aggressiveMode == true)
            {
                //deactivated
                this.aggressiveMode = false;
                if (base.AttackPoints >= 50)
                {
                    base.attackPoints -= 50;
                    base.defensePoints += 25;
                }   
                else
                {   
                    base.defensePoints += 25;
                    base.attackPoints = 0;
                }
            }
            else if (this.aggressiveMode == false)
            {
                //activated
                this.aggressiveMode = true;
                if (base.DefensePoints>= 25)
                {   
                    base.attackPoints += 50;
                    base.defensePoints -= 25;
                }   
                else
                {   
                    
                    base.attackPoints += 50;
                    base.defensePoints =0;
                }
            }
        }

        public override string ToString()
        {
            return base.ToString() 
                +System.Environment.NewLine
                + $" *Aggressive: {(this.AggressiveMode==true?"ON":"OFF")}";
        }
    }
}
