using System;
using System.Collections.Generic;
using System.Threading;
using HerculesBattle.Models;
using HerculesBattle.Core;
using HerculesBattle.Enums;
using System.Net.Http.Headers;
using System.Data;

namespace HerculesBattle.Managers
{
    public class GameManager
    {
        private Character player;
        private List<Level>? levels;
        private GameSettings? settings;
        private int currentLevel;

        public GameManager()
        {
            player = new Character("Hero");
            levels = new List<Level>();
            settings = new GameSettings();
            currentLevel = 0;
        }

        private static void ShowGameIntro()
        {
            Console.Clear();
            Console.WriteLine(@"
        ╔════════════════════════════════════════════╗
        ║      HERCULES: BATTLE OF THE IMMORTALS     ║
        ╚════════════════════════════════════════════╝

        _    _ ______ _____   _____ _    _ _      ______  _____ 
        | |  | |  ____|  __ \ / ____| |  | | |    |  ____|/ ____|
        | |__| | |__  | |__) | |    | |  | | |    | |__  | (___  
        |  __  |  __| |  _  /| |    | |  | | |    |  __|  \___ \ 
        | |  | | |____| | \ \| |____| |__| | |____| |____ ____) |
        |_|  |_|______|_|  \_\\_____|\_____/|______|______|_____/ 
                                                                
            

        Available Weapons:
            --SWORD--  
            --SPEAR--  
            --BOW--    
            --AXE--    

        Press any key to begin your battle...");
            Console.ReadKey();
        }

        private List<Weapon> GetAvailableWeapons()
        {
            var weapons = new List<Weapon>();

            // Basic weapons (from level 1 - 10)
            weapons.Add(new Weapon(WeaponType.Sword));
            weapons.Add(new Weapon(WeaponType.Spear));

            // Bow available from levels 5-8
            if (currentLevel >= 5)
            {
                weapons.Add(new Weapon(WeaponType.Bow));
            }

            // Axe available from level 9-10
            if (currentLevel >= 9)
            {
                weapons.Add(new Weapon(WeaponType.Axe));
            }

            return weapons;
        }

        private Weapon ChooseWeapon()
        {
            var availableWeapons = GetAvailableWeapons();
            bool validInput = false;
            int choice = 0;

            while (!validInput)
            {
                Console.WriteLine($"\nAvailable Weapons in your Inventory:");
                for (int i = 0; i < availableWeapons.Count; i++)
                {
                    var weapon = availableWeapons[i];
                    Console.WriteLine($"{i + 1}. {weapon.Type} - {weapon.Description} (Attack Bonus: +{weapon.AttackBonus})");
                }

                Console.Write($"\nSelect your weapon (1-{availableWeapons.Count}): ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= availableWeapons.Count)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine($"\nInvalid input! Please input number between 1 and {availableWeapons.Count}.");
                }
            }
            return availableWeapons[choice - 1];
        }

        private void InitializeGame()
        {
            ShowGameIntro();

            Console.Write("\nEnter your name or 'Enter' for Hercules): ");
            string? playerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(playerName)) playerName = "Hercules";

            player = new Character(playerName);
            settings = new GameSettings();
            currentLevel = 1;
            InitializeLevels();
        }

        private void InitializeLevels()
        {
            levels = new List<Level>
                {
                    // Level 1-4: Battle with Soldier at City of Tiryns (2 weapons available)
                    new Level(1, "Villager", "A Villager at City of Tiryns", EnemyType.Human),
                    new Level(2, "Veteran Soldier", "An experienced soldier", EnemyType.Human),
                    new Level(3, "Elite Guard", "One of the city's finest warriors", EnemyType.Human),
                    new Level(4, "Captain of the Guard", "Leader of the city guards", EnemyType.Human),
                    
                    // Level 5-8: King Eurystheus and The Elite Warriors of Eurystheus (3 weapons available)
                    new Level(5, "Royal Guard", "Eurystheus's personal guard", EnemyType.EliteWarrior),
                    new Level(6, "Royal Champion", "Champion of Eurystheus", EnemyType.EliteWarrior),
                    new Level(7, "Eurystheus's General", "Right hand of Eurystheus", EnemyType.EliteWarrior),
                    new Level(8, "King Eurystheus", "The King", EnemyType.Boss),
                    
                    // Level 9-10: Help Zeus to defeat The Giants of Olympus (all 4 weapons available)
                    new Level(9, "Lesser Giant", "A formidable giant", EnemyType.Giant),
                    new Level(10, "Giant King", "Leader of the giants", EnemyType.Giant)
                };
        }


        // Game Start
        public void StartGame()
        {
            InitializeGame();

            ShowIntroduction();

            if (levels == null || levels.Count == 0)
            {
                Console.WriteLine("No levels available.");
                return;
            }


            currentLevel = 1; // CurrentLevel initialized

            while (currentLevel <= levels.Count && player.IsAlive())
            {
                Level level = levels[currentLevel - 1];
                Console.WriteLine($"\n=== Level {currentLevel}: {level.Name} ===");
                Console.WriteLine(level.Description);

                // Select weapon before battle
                Weapon weapon = ChooseWeapon();
                player.EquipWeapon(weapon);

                Enemy enemy = CreateEnemy(level);
                Battle battle = new Battle(player, enemy);

                PrepareForBattle();
                battle.Start();

                if (player.IsAlive())
                {
                    currentLevel++;
                    if (currentLevel <= levels.Count)
                    {
                        bool continuePlaying = ShowLevelCompleteMenu();
                        if (!continuePlaying)
                        {
                            ShowGameOverMessage();
                            break;
                        }
                    }
                    else
                    {
                        ShowVictoryMessage();
                    }
                }
                else
                {
                    ShowDefeatMessage();
                    break;
                }
            }
        }

        private Enemy CreateEnemy(Level level)
        {
            int enemyLevel = currentLevel;
            int healthMultiplier = level.EnemyType == EnemyType.Boss ? 2 :
                                level.EnemyType == EnemyType.Giant ? 3 : 1;

            return new Enemy(level.Name, enemyLevel, healthMultiplier);
        }

        private void PrepareForBattle()
        {
            Console.WriteLine("\nPreparing for battle...");
            Console.WriteLine("1. View Status");
            Console.WriteLine("2. View Items");
            Console.WriteLine("3. Start The Battle");

            bool ready = false;
            while (!ready)
            {
                Console.Write("\nYour choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowPlayerStatus();
                        break;
                    case "2":
                        ViewItems();
                        break;
                    case "3":
                        ready = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Selec from (1-3)");
                        break;
                }
            }
        }

        private void ShowPlayerStatus()
        {
            Console.WriteLine($"\n=== {player.Name}'s Status ===");
            Console.WriteLine($"Level: {player.Level}");
            Console.WriteLine($"Health: {player.Health}/{player.MaxHealth}");
            Console.WriteLine($"Energy: {player.Energy}/{player.MaxEnergy}");
            Console.WriteLine($"Attack: {player.AttackPower}");
            Console.WriteLine($"Defense: {player.Defense}");
            Console.WriteLine($"Speed: {player.Speed}");
            Console.WriteLine($"Experience: {player.Experience}/{player.ExperienceToNextLevel}");
            if (player.EquippedWeapon != null)
            {
                Console.WriteLine($"Equipped Weapon: {player.EquippedWeapon.Type}");
            }
        }

        private void UseItems()
        {
            Console.WriteLine("\nNo items available yet.");
        }

        private void ViewItems()
        {
            Console.WriteLine("Divine Sword: An absolute sword that deals twice base damage to enemies.");
            Console.WriteLine("Queen's Life: Instantly restores the player's health to maximum.");
            Console.WriteLine("Adamant Shield: An extremely strong and unbreakable material.");
            Console.WriteLine("Each item has its own unique ability and the player can only use it once.");
        }

        private void ShowIntroduction()
        {
            Console.WriteLine("\nIn ancient Greece, Hercules, son of Zeus and Alcmene, seeks redemption...");
            Console.WriteLine("To become an Immortal and right the wrongs of his past, he must prove himself in battle...");
            Thread.Sleep(2000);
        }

        private bool ShowLevelCompleteMenu()
        {
            Console.WriteLine("\n╔════════════════════════╗");
            Console.WriteLine("║    Level Complete!     ║");
            Console.WriteLine("╚════════════════════════╝");
            Console.WriteLine("1. Continue to next level");
            Console.WriteLine("2. End game");

            string choice = string.Empty;
            do
            {
                Console.Write("\nYour choice (1-2): ");
                choice = Console.ReadLine() ?? string.Empty;
            } while (choice != "1" && choice != "2");

            return choice == "1";
        }

        private void ShowVictoryMessage()
        {
            Console.Clear();
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║         CONGRATULATIONS!!!            ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine("\nYou have completed all challenges!");
            Console.WriteLine($"Hercules has proven himself worthy of immortality!");
            Console.WriteLine($"\nFinal Stats:");
            Console.WriteLine($"Level achieved: {player.Level}");
            Console.WriteLine($"Total Experience: {player.Experience}");
            if (player.EquippedWeapon != null)
            {
                Console.WriteLine($"Final Weapon: {player.EquippedWeapon.Type}");
            }
            else
            {
                Console.WriteLine("Final Weapon: None");
            }
            Console.WriteLine("\nThank you for playing!");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private void ShowDefeatMessage()
        {
            Console.Clear();
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║            GAME OVER                  ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine("\nHercules has fallen in battle...");
            Console.WriteLine($"You reached Level {currentLevel}");
            Console.WriteLine($"Final Experience: {player.Experience}");
            if (player.EquippedWeapon != null)
            {
                Console.WriteLine($"Final Weapon: {player.EquippedWeapon.Type}");
            }
            else
            {
                Console.WriteLine("Final Weapon: None");
            }
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private void ShowGameOverMessage()
        {
            Console.Clear();
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║            GAME OVER                  ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine("\nYou chose to end your journey...");
            Console.WriteLine($"You reached Level {currentLevel}");
            Console.WriteLine($"Final Experience: {player.Experience}");
            if (player.EquippedWeapon != null)
            {
                Console.WriteLine($"Final Weapon: {player.EquippedWeapon.Type}");
            }
            else
            {
                Console.WriteLine("Final Weapon: None");
            }
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    public class Level
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public EnemyType EnemyType { get; private set; }

        public Level(int level, string name, string description, EnemyType enemyType)
        {
            Name = name;
            Description = description;
            EnemyType = enemyType;
        }
    }
}