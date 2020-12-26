using System.Collections.Generic;

namespace PlayersAndMonsters
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Hero> heroes = new List<Hero>();

            Hero hero = new Elf("Go",1);
            Hero hero1 = new Knight("So", 2);
            Hero hero2 = new Wizard("Ho", 3);

            heroes.Add(hero);
            heroes.Add(hero1);
            heroes.Add(hero2);

            System.Console.WriteLine(hero);
        }
    }
}