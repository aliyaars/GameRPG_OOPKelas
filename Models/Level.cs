using HerculesBattle.Enums;

namespace HerculesBattle.Models
{
    public class Level
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public EnemyType EnemyType { get; private set; }
        public int LevelNumber { get; private set; }
        public int RequiredPlayerLevel { get; private set; }

        public Level(int level, string name, string description, EnemyType enemyType)
        {
            LevelNumber = level;
            Name = name;
            Description = description;
            EnemyType = enemyType;
            RequiredPlayerLevel = level;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"\n=== Level {LevelNumber}: {Name} ===");
            Console.WriteLine(Description);
            Console.WriteLine($"Enemy Type: {EnemyType}");
            Console.WriteLine($"Required Player Level: {RequiredPlayerLevel}");
        }
    }
}