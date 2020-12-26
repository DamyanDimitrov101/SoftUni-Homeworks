using System;
using System.Collections.Generic;
using System.Text;

namespace ViceCity.Models.Guns
{
    public class Pistol : Gun
    {
        private const int pistolBulletsPerBarrel = 10;
        private const int pistolTotalBullets= 100;
        private const int pistolShotBullets = 1;

        public Pistol(string name) : base(name, pistolBulletsPerBarrel, pistolTotalBullets)
        {
        }

        public override int Fire()
        {
    
            int shotTotal = 0;

            if (this.BulletsPerBarrel <= 0)
            {
                this.Reloading();
            }

            shotTotal += pistolShotBullets;
            this.BulletsPerBarrel = Math.Max(0, this.BulletsPerBarrel - pistolShotBullets);
            
            if (this.BulletsPerBarrel <= 0)
            {
                this.Reloading();
            }
            return shotTotal;
            
        }

        private void Reloading()
        {
            int bulletsToReload = this.CapacityOfTheBarrel;

            if (this.TotalBullets >= pistolShotBullets)
            {
                this.TotalBullets -= bulletsToReload;
                this.BulletsPerBarrel = bulletsToReload;
            }
        }
    }
}
