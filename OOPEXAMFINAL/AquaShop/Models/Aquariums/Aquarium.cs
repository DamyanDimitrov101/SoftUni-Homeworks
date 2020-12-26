using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private int capacity;
        private List<IDecoration> decorations;
        private List<IFish> fish;

        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.decorations = new List<IDecoration>();
            this.fish = new List<IFish>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Aquarium name cannot be null or empty.");
                }
                this.name = value;
            }
        }

        public int Capacity { get => this.capacity; private set => this.capacity = value; }

        public int Comfort => this.decorations.Sum(d=>d.Comfort);

        public ICollection<IDecoration> Decorations => this.decorations.AsReadOnly();

        public ICollection<IFish> Fish
        {
            get { return this.fish; }
            protected set => this.fish = value.ToList();
        }

        public void AddDecoration(IDecoration decoration)
        {
            if (decoration!=null)
            {
                this.decorations.Add(decoration);
            }
        }

        public abstract void AddFish(IFish fish);

        public void Feed()
        {
            foreach (var fi in this.fish)
            {
                fi.Eat();
            }
        }

        public string GetInfo()
        {
            var sb = new StringBuilder();

            
             sb.AppendLine($"{this.Name} ({this.GetType().Name}):");
             sb.AppendLine($"Fish: {(this.fish.Count == 0? "none":string.Join(", ",this.fish))}");
             sb.AppendLine($"Decorations: {this.Decorations.Count}");
             sb.AppendLine($"Comfort: {this.Comfort}");

           

            return sb.ToString();
        }

        public bool RemoveFish(IFish fish)
        {
            return this.fish.Remove(fish);
        }
    }
}
