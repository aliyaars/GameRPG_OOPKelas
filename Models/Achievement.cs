namespace HerculesBattle.Models
{
    public class Achievement
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsUnlocked { get; private set; }

        public Achievement(string name, string description)
        {
            Name = name;
            Description = description;
            IsUnlocked = false;
        }

        public void Unlock()
        {
            IsUnlocked = true;
        }

        public override string ToString()
        {
            return $"{Name} - {Description} (Unlocked: {IsUnlocked})";
        }
    }
}
