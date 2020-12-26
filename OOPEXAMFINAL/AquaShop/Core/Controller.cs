using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository decorationRepository;
        private List<IAquarium> aquariums;
        public Controller()
        {
            this.decorationRepository = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType == "FreshwaterAquarium")
            {
                IAquarium aquarium = new FreshwaterAquarium(aquariumName);
                this.aquariums.Add(aquarium);
                return $"Successfully added {aquariumType}.";
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                IAquarium aquarium = new SaltwaterAquarium(aquariumName);
                this.aquariums.Add(aquarium);
                return $"Successfully added {aquariumType}.";
            }
            else
            {
                throw new InvalidOperationException("Invalid aquarium type.");
            }
        }

        public string AddDecoration(string decorationType)
        {
            if (decorationType == "Plant" || decorationType == "Ornament")
            {
                IDecoration decoration = null;
                if (decorationType == "Plant")
                {
                    decoration = new Plant();
                }
                else if (decorationType == "Ornament")
                {
                    decoration = new Ornament();
                }

                this.decorationRepository.Add(decoration);
                return $"Successfully added {decorationType}.";
            }
            else
            {
                throw new InvalidOperationException("Invalid decoration type.");
            }
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IAquarium aquarium = this.aquariums.First(a => a.Name == aquariumName);

            if (fishType == "FreshwaterFish" || fishType == "SaltwaterFish")
            {
                if ((fishType == "FreshwaterFish" && aquarium is FreshwaterAquarium) || (fishType == "SaltwaterFish" && aquarium is SaltwaterAquarium))
                {
                    IFish fish = null;
                    if (fishType == "FreshwaterFish")
                    {
                        fish = new FreshwaterFish(fishName, fishSpecies, price);
                    }
                    else if (fishType == "SaltwaterFish")
                    {
                        fish = new SaltwaterFish(fishName, fishSpecies, price);
                    }
                    aquarium.AddFish(fish);
                    return $"Successfully added {fishType} to {aquariumName}.";
                }
                else
                {
                    return "Water not suitable.";
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid fish type.");
            }
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = this.aquariums.First(a => a.Name == aquariumName);
            decimal valueToReturn = aquarium.Fish.Sum(f => f.Price) + aquarium.Decorations.Sum(d => d.Price);
            return $"The value of Aquarium {aquariumName} is {valueToReturn:F2}.";
        }

        public string FeedFish(string aquariumName)
        {
            int fishFed = 0;

            IAquarium aquarium = this.aquariums.First(a => a.Name == aquariumName);

            foreach (var fi in aquarium.Fish)
            {
                fi.Eat();
                fishFed++;
            }

            return $"Fish fed: {fishFed}";
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IAquarium aquarium = this.aquariums.First(a => a.Name == aquariumName);

            IDecoration decoration = this.decorationRepository.Models.FirstOrDefault(d => d.GetType().Name == decorationType);

            if (decoration is null)
            {
                throw new InvalidOperationException($"There isn't a decoration of type {decorationType}.");
            }

            this.decorationRepository.Remove(decoration);

            aquarium.AddDecoration(decoration);

            return $"Successfully added {decorationType} to {aquariumName}.";
        }

        public string Report()
        {
            var sb = new StringBuilder();
            foreach (var aquarim in this.aquariums)
            {
                sb.Append(aquarim.GetInfo());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
