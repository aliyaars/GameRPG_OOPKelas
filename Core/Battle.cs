using System;
using System.Threading;
using HerculesBattle.Models;

namespace HerculesBattle.Core
{
    public class Battle
    {
        private Character player;
        private Enemy enemy;
        private bool isPlayerTurn;

        public Battle(Character player, Enemy enemy)
        {
            this.player = player ?? throw new ArgumentNullException(nameof(player), "Player cannot be null.");
            this.enemy = enemy ?? throw new ArgumentNullException(nameof(enemy), "Enemy cannot be null.");
            isPlayerTurn = player.Speed >= enemy.Speed;
        }


        public void Start()
        {
            if (player == null || enemy == null)
            {
                Console.WriteLine("Error: Player or Enemy is not initialized.");
                return;
            }

            Console.WriteLine($"\nBattle starts! {player.Name} vs {enemy.Name}");
            Console.WriteLine($"Enemy Level: {enemy.Level}");
            Console.WriteLine($"Enemy Health: {enemy.Health}");
            
            while (player.IsAlive() && enemy.IsAlive())
            {
                if (isPlayerTurn)
                {
                    PlayerTurn();
                }
                else
                {
                    EnemyTurn();
                }

                isPlayerTurn = !isPlayerTurn;
                Thread.Sleep(1000);
            }

            EndBattle();
        }

        private void PlayerTurn()
        {
            if (player == null || enemy == null)
            {
                Console.WriteLine("Error: Player or Enemy is not initialized.");
                return;
            }

            Console.WriteLine($"\n{player.Name}'s turn!");
            Console.WriteLine($"Health: {player.Health}/{player.MaxHealth}");
            Console.WriteLine($"Energy: {player.Energy}/{player.MaxEnergy}");
            Console.WriteLine("\nActions:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Defend");
            Console.WriteLine("3. Use Item");

            string choice;
            do
            {
                Console.Write("\nChoose your action (1-3): ");
                choice = Console.ReadLine();
            } while (choice != "1" && choice != "2" && choice != "3");

            switch (choice)
            {
                case "1":
                    PerformAttack(player.AttackPower, enemy);
                    break;
                case "2":
                    Defend();
                    break;
                case "3":
                    UseItem();
                    break;
            }
        }

        private void EnemyTurn()
        {
            Console.WriteLine($"\n{enemy.Name}'s turn!");
            PerformAttack(enemy.AttackPower, player);
        }

            private void PerformAttack(int attackPower, object target)
        {
            if (target == null)
            {
                Console.WriteLine("Error: Attack target is null.");
                return;
            }

            Random random = new Random();
            int chance = random.Next(1, 101); // 1-100 untuk peluang critical/miss

            if (chance <= 10) // 10% chance to miss
            {
                Console.WriteLine("The attack missed!");
                return;
            }

            if (chance >= 90) // 10% chance for critical hit
            {
                attackPower *= 2;
                Console.WriteLine("Critical hit!");
            }

            if (target is Character character)
            {
                character.TakeDamage(attackPower);
            }
            else if (target is Enemy enemy)
            {
                enemy.TakeDamage(attackPower);
            }
            else
            {
                Console.WriteLine("Invalid target for attack.");
            }
        }

        private void Defend()
        {
            Console.WriteLine($"{player.Name} takes a defensive stance!");
            player.RestoreEnergy(20);
        }

        private void UseItem()
        {
            Console.WriteLine($"{player.Name} uses a healing herb!");
            player.Heal(20);
        }

        private void EndBattle()
        {
            if (player.IsAlive())
            {
                Console.WriteLine($"\nVictory! {player.Name} has defeated {enemy.Name}!");
                player.GainExperience(enemy.ExperienceValue);
            }
            else
            {
                Console.WriteLine($"\nDefeat! {player.Name} has been defeated!");
            }
        }
    }
}
