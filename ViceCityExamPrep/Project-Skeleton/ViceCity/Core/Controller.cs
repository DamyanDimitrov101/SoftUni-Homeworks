using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViceCity.Core.Contracts;
using ViceCity.Models.Guns;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Neghbourhoods;
using ViceCity.Models.Neghbourhoods.Contracts;
using ViceCity.Models.Players;
using ViceCity.Models.Players.Contracts;

namespace ViceCity.Core
{
    public class Controller : IController
    {
        private MainPlayer mainPlayer;

        private IList<IPlayer> civilPlayers;
        private IList<IGun> guns;

        private INeighbourhood neighbourhood;
        
        public Controller()
        {
            this.mainPlayer = new MainPlayer();
            this.civilPlayers = new List<IPlayer>();
            this.guns= new List<IGun>();

            this.neighbourhood = new GangNeighbourhood();
        }
        public string AddGun(string type, string name)
        {
            if (type=="Pistol")
            {
                Pistol pistol = new Pistol(name);
                this.guns.Add(pistol);
                return $"Successfully added {name} of type: {type}";
            }
            else if (type == "Rifle")
            {
                Rifle rifle = new Rifle(name);
                this.guns.Add(rifle);
                return $"Successfully added {name} of type: {type}";
            }
            else
            {
                return "Invalid gun type!";
            }
        }

        public string AddGunToPlayer(string name)
        {
            if (this.guns.Count==0)
            {
                return "There are no guns in the queue!";
            }

            IGun gun = this.guns[0];

            if (name== "Vercetti")
            {
                mainPlayer.GunRepository.Add(gun);
                this.guns.Remove(gun);
                return $"Successfully added {gun.Name} to the Main Player: Tommy Vercetti";
            }

            IPlayer civilPl = this.civilPlayers.FirstOrDefault(p=>p.Name==name);

            if (civilPl==null)
            {
                return "Civil player with that name doesn't exists!";
            }

            civilPl.GunRepository.Add(gun);
            this.guns.Remove(gun);
            return $"Successfully added {gun.Name} to the Civil Player: {civilPl.Name}";
        }

        public string AddPlayer(string name)
        {
            CivilPlayer civilPlayer = new CivilPlayer(name);

            civilPlayers.Add(civilPlayer);

            return $"Successfully added civil player: {name}!";
        }

        public string Fight()
        {
            int totalHealthPointsCivils = this.civilPlayers.Sum(c => c.LifePoints);

            this.neighbourhood.Action(mainPlayer,civilPlayers);

            int totalHealthPointsCivilsAfter = this.civilPlayers.Sum(c => c.LifePoints);
            int countOfKilledCivils = this.civilPlayers.Where(p => !p.IsAlive).ToList().Count;

            if (mainPlayer.LifePoints==100 && totalHealthPointsCivils==totalHealthPointsCivilsAfter)
            {
                return "Everything is okay!";
            }

            var sb = new StringBuilder();

            sb.AppendLine("A fight happened:");
            sb.AppendLine($"Tommy live points: {mainPlayer.LifePoints}!");
            sb.AppendLine($"Tommy has killed: {countOfKilledCivils} players!");
            sb.AppendLine($"Left Civil Players: {this.civilPlayers.Count-countOfKilledCivils}!");

            return sb.ToString().TrimEnd();
        }
    }
}
