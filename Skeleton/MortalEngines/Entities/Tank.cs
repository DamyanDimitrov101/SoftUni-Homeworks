using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public class Tank : BaseMachine, ITank
    {
        private bool defenseMode = false;

        public Tank(string name, double attackPoints, double defensePoints) : base(name, attackPoints, defensePoints, 100)
        {
            this.ToggleDefenseMode();
        }

        public bool DefenseMode { get => this.defenseMode; }

        public void ToggleDefenseMode()
        {
            if (this.DefenseMode == true)
            {
                //deactivated
                this.defenseMode= false;
                if (base.DefensePoints >= 30)
                {
                    base.attackPoints += 40;
                    base.defensePoints -= 30;
                }
                else
                {
                    base.defensePoints = 0;
                    base.attackPoints += 40;
                }
            }
            else
            {
                //activated
                this.defenseMode= true;
                if (base.AttackPoints >= 40)
                {
                    base.attackPoints -= 40;
                    base.defensePoints += 30;
                }
                else
                {

                    base.attackPoints = 0;
                    base.defensePoints += 30;
                }
            }
        }

        public override string ToString()
        {
            return base.ToString()
                +System.Environment.NewLine
                + $" *Defense: {(this.DefenseMode==true?"ON":"OFF")}";
        }
    }
}
