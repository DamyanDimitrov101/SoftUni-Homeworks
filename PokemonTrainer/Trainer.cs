using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonTrainer
{
    public class Trainer
    {
        string name;
        int numBadges;
        List<Pokemon> pokemons;

        public Trainer(string name, int numBadges, List<Pokemon> pokemons)
        {
            Name = name;
            NumBadges = numBadges;
            Pokemons = pokemons;
        }

        public Trainer()
        {

        }

        public string Name { get => name; set => name = value; }
        public int NumBadges { get => numBadges; set => numBadges = value; }
        public List<Pokemon> Pokemons { get => pokemons; set => pokemons = value; }

        public override string ToString()
        {
            return $"{this.Name} {this.NumBadges} {this.Pokemons.Count}";
        }
    }
}
