using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Models.Players;
using PlayersAndMonsters.Models.Players.Contracts;
using System;
using System.Linq;

namespace PlayersAndMonsters.Models.BattleFields
{
    public class BattleField : IBattleField
    {
        public void Fight(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            CheckIfAPlayerIsDeadBeforeStartOfTheFigh(attackPlayer, enemyPlayer);

            CheckIfPlayerIsBeginner(attackPlayer);
            CheckIfPlayerIsBeginner(enemyPlayer);

            attackPlayer.Health+=attackPlayer.CardRepository.Cards.Sum(c => c.HealthPoints);
            enemyPlayer.Health+= enemyPlayer.CardRepository.Cards.Sum(c => c.HealthPoints);

            var attackerDamagePoints = attackPlayer.CardRepository.Cards.Sum(c => c.DamagePoints);
            var enemyDamagePoints = enemyPlayer.CardRepository.Cards.Sum(c => c.DamagePoints);

            while (true)
            {
                enemyPlayer.TakeDamage(attackerDamagePoints);

                if (attackPlayer.IsDead || enemyPlayer.IsDead)
                {
                    break;
                }


                attackPlayer.TakeDamage(enemyDamagePoints);

                if (attackPlayer.IsDead || enemyPlayer.IsDead)
                {
                    break;
                }
            }
        }

        private static void CheckIfAPlayerIsDeadBeforeStartOfTheFigh(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if (attackPlayer.IsDead || enemyPlayer.IsDead)
            {
                throw new ArgumentException("Player is dead!");
            }
        }

        private static void CheckIfPlayerIsBeginner(IPlayer player)
        {
            if (player is Beginner)
            {
                player.Health += 40;
                foreach (var card in player.CardRepository.Cards)
                {
                    card.DamagePoints += 30;
                }
            }
        }
    }
}
