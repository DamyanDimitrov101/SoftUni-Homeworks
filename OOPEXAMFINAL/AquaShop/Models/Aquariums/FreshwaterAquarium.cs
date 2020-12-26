using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public class FreshwaterAquarium : Aquarium
    {
        private const int capacityFreshInitial = 50;
        public FreshwaterAquarium(string name) : base(name, capacityFreshInitial)
        {
        }

        public override void AddFish(IFish fish)
        {
            if (this.Capacity>this.Fish.Count && fish is FreshwaterFish)
            {
                    this.Fish.Add(fish);
            }
            else
            {
                throw new InvalidOperationException("Not enough capacity.");
            }
        }
    }
}
