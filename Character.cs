namespace HerculesBattle.Models
{
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int Energy { get; private set; }
        public int MaxEnergy { get; private set; }
        public int AttackPower { get; private set; }
        public int Defense { get; private set; }
        public int Speed { get; private set; }
        public int Experience { get; private set; }
        public int ExperienceToNextLevel { get; private set; }

        public Character(string name)
        {
            Name = name;
            Level = 1;
            MaxHealth = 100;
            Health = MaxHealth;
            MaxEnergy = 100;
            Energy = MaxEnergy;
            AttackPower = 10;
            Defense = 5;
            Speed = 5;
            Experience = 0;
            ExperienceToNextLevel = 100;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            Health = Math.Max(0, Health - actualDamage);
            Console.WriteLine($"{Name} takes {actualDamage} damage!");
        }

        public bool IsAlive() => Health > 0;

        public void Heal(int amount)
        {
            Health = Math.Min(MaxHealth, Health + amount);
            Console.WriteLine($"{Name} heals for {amount} HP!");
        }

        public void UseEnergy(int amount)
        {
            Energy = Math.Max(0, Energy - amount);
        }

        public void RestoreEnergy(int amount)
        {
            Energy = Math.Min(MaxEnergy, Energy + amount);
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            Console.WriteLine($"{Name} gains {amount} experience!");
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            while (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Experience -= ExperienceToNextLevel;
            ExperienceToNextLevel = Level * 100;

            MaxHealth += 20;
            Health = MaxHealth;
            MaxEnergy += 10;
            Energy = MaxEnergy;
            AttackPower += 5;
            Defense += 3;
            Speed += 2;

            Console.WriteLine($"\n{Name} has reached level {Level}!");
            Console.WriteLine($"New stats:");
            Console.WriteLine($"Health: {MaxHealth}");
            Console.WriteLine($"Attack: {AttackPower}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Speed: {Speed}\n");
        }
    }
}