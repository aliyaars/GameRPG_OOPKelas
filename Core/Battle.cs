using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using HerculesBattle.Enums;
using HerculesBattle.Models;

namespace HerculesBattle.Core
{
    public class Battle
    {
        private Character player;
        private Enemy enemy;
        private bool isPlayerTurn;

        private bool Item1Used = false, Item2Used = false, Item3Used = false, Item3Once = false;


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
                choice = Console.ReadLine() ?? string.Empty;
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
            if (Item3Used && Item3Once)
            {
                Console.WriteLine($"{player.Name} is using Adamant Shield, {enemy.Name} attack is blocked!");
                Item3Once = false;
            }
            else
            {
                PerformAttack(enemy.AttackPower, player);
            }
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
            Console.WriteLine("Choose an item to use:");
            Console.WriteLine("1. Divene Sword");
            Console.WriteLine("2. Queen's Life");
            Console.WriteLine("3. Adamant Shield");
            Console.WriteLine("Select: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (!Item1Used)
                    {
                        PerformAttack(player.AttackPower * 2, enemy);
                        Item1Used = true;
                    }
                    else
                    {
                        Console.WriteLine("You have used this item, preapare to be attack by the enemy!");
                    }
                    break;
                case "2":
                    if (!Item2Used)
                    {
                        player.Heal(player.MaxHealth);
                        Item2Used = true;
                    }
                    else
                    {
                        Console.WriteLine("You have used this item, preapare to be attack by the enemy!");
                    }
                    break;
                case "3":
                    if (!Item3Used && !Item3Once)
                    {
                        // efek akan dilakukan di EnemyTurn
                        Item3Used = true;
                        Item3Once = true;
                    }
                    else
                    {
                        Console.WriteLine("You have used this item, preapare to be attack by the enemy!");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

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
