using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTrainer
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Dictionary<string, Trainer> trainers = new Dictionary<string, Trainer>();

            string input = Console.ReadLine();

            while (input!= "Tournament")
            {
                string[] arr = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string nameTrainer = arr[0];
                string pokemon = arr[1];
                string element = arr[2];
                int healthPokemon = int.Parse(arr[3]);

                Pokemon current = new Pokemon(pokemon, element, healthPokemon);

                

                if (!trainers.ContainsKey(nameTrainer))
                {
                    trainers[nameTrainer] = new Trainer(nameTrainer, 0, new List<Pokemon>()); 
                }

                trainers[nameTrainer].Pokemons.Add(current);

                input = Console.ReadLine();
            }

            string secondInput = Console.ReadLine();

            while (secondInput!="End")
            {
                trainers = CheckTrainer(trainers, secondInput);

                secondInput = Console.ReadLine();
            }

            foreach (var trainer in trainers.OrderByDescending(t=>t.Value.NumBadges))
            {
                Console.WriteLine(trainer.Value);
            }
        }

        private static Dictionary<string, Trainer> CheckTrainer(Dictionary<string, Trainer> trainers, string secondInput)
        {
            foreach (var trainer in trainers.Values)
            {
                if (trainer.Pokemons.Any(p => p.Element == secondInput))
                {
                    trainer.NumBadges++;
                }
                else
                {
                    foreach (var pok in trainer.Pokemons)
                    {
                        pok.Health -= 10;
                    }
                }

            }

            foreach (var tr in trainers.Values)
            {

                for (int i = 0; i < tr.Pokemons.Count; i++)
                {
                    if (tr.Pokemons[i].Health<=0)
                    {
                        tr.Pokemons.RemoveAt(i);
                        i--;
                    }
                }
            }



            return trainers;
        }
    }
}
