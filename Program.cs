using HerculesBattle.Models;
using HerculesBattle.Systems;

namespace HerculesBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hercules: Battle of the Immortals!");
            Console.WriteLine("----------------------------------------");
            
            // Initialize game
            Console.WriteLine("Initializing game...");
            Character hercules = new Character("Hercules");
            GameSettings settings = new GameSettings();
            Inventory inventory = new Inventory(20);

            // Add some initial items
            inventory.AddItem(new Item("Health Potion", "Restores 50 HP", ItemType.HealingPotion, 50));
            inventory.AddItem(new Item("Bronze Sword", "Basic weapon", ItemType.Weapon, 10));

            Console.WriteLine("\nHercules has been created!");
            Console.WriteLine($"Level: {hercules.Level}");
            Console.WriteLine($"Health: {hercules.Health}");
            Console.WriteLine("\nPress Enter to start your first battle!");
            Console.ReadLine();

            // Start first battle
            Enemy firstEnemy = new Enemy("Training Dummy", 1);
            Battle battle = new Battle(hercules, firstEnemy);
            battle.Start();
        }
    }
}