using System;
using System.Collections.Generic;
using System.Text;

namespace ViceCity.Models.Guns
{
    public class Rifle : Gun
    {
        private const int rifleBulletsPerBarrel = 50;
        private const int rifleTotalBullets = 500;
        private const int rifleShotBullets = 5;

        public Rifle(string name) : base(name, rifleBulletsPerBarrel, rifleTotalBullets)
        {
        }

        public override int Fire()
        {
            int shotTotal = 0;

            if (this.BulletsPerBarrel <= 0)
            {
                this.Reloading();
            }

            shotTotal += rifleShotBullets;
            
            this.BulletsPerBarrel = Math.Max(0, this.BulletsPerBarrel - rifleShotBullets);
            
            if (this.BulletsPerBarrel <= 0)
            {
                this.Reloading();
            }
            return shotTotal;

        }

        private void Reloading()
        {
            int bulletsToReload = this.CapacityOfTheBarrel;

            if (this.TotalBullets>=rifleShotBullets)
            {
                this.TotalBullets -= bulletsToReload;
                this.BulletsPerBarrel = bulletsToReload;
            }
        }
    }
}
