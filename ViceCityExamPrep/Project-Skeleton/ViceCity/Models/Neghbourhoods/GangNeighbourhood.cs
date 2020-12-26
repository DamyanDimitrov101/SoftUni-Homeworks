using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Neghbourhoods.Contracts;
using ViceCity.Models.Players;
using ViceCity.Models.Players.Contracts;

namespace ViceCity.Models.Neghbourhoods
{
    public class GangNeighbourhood : INeighbourhood
    {
        public void Action(IPlayer mainPlayer, ICollection<IPlayer> civilPlayers)
        {
            foreach (var gun in mainPlayer.GunRepository.Models)
            {
                foreach (var civil in civilPlayers)
                {
                    if (!gun.CanFire)
                    {
                        break;
                    }

                    if (!civil.IsAlive)
                    {
                        continue;
                    }

                    while (gun.TotalBullets > 0 || gun.BulletsPerBarrel > 0)
                    {
                        int damage = gun.Fire();

                        civil.TakeLifePoints(damage);

                        if (!civil.IsAlive)
                        {
                            break;
                        }

                        if (gun.TotalBullets <= 0 && gun.BulletsPerBarrel <= 0)
                        {
                            break;
                        }
                    }

                }
            }


            foreach (var civil in civilPlayers.Where(c=>c.IsAlive))
            {
                if (!mainPlayer.IsAlive)
                {
                    break;
                }
                foreach (var gun in civil.GunRepository.Models)
                {
                    if (!gun.CanFire)
                    {
                        continue;
                    }

                    if (!mainPlayer.IsAlive)
                    {
                        break;
                    }

                    while (gun.TotalBullets > 0 || gun.BulletsPerBarrel > 0)
                    {
                        int damage = gun.Fire();

                        mainPlayer.TakeLifePoints(damage);

                        if (!mainPlayer.IsAlive)
                        {
                            break;
                        }

                        if (gun.TotalBullets <= 0 && gun.BulletsPerBarrel <= 0)
                        {
                            break;
                        }
                    }
                }
            }
        }

        
    }
}
