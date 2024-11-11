namespace HerculesBattle.Models
{
    public class Enemy
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int AttackPower { get; private set; }
        public int Defense { get; private set; }
        public int Speed { get; private set; }
        public int ExperienceValue { get; private set; }

        public Enemy(string name, int level)
        {
            Name = name;
            Level = level;
            MaxHealth = 50 + (level * 20);
            Health = MaxHealth;
            AttackPower = 5 + (level * 3);
            Defense = 2 + (level * 2);
            Speed = 3 + level;
            ExperienceValue = level * 50;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            Health = Math.Max(0, Health - actualDamage);
        }

        public bool IsAlive() => Health > 0;
    }
}