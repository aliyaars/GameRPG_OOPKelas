using HerculesBattle.Enums;
using HerculesBattle.Managers;

namespace HerculesBattle.Models
{
    public class Weapon
    {
        public WeaponType Type { get; private set; }
        public int AttackBonus { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public int MinLevel { get; private set; } = 0;

        public Weapon(WeaponType type, AchievementManager? achievementManager = null)
        {
            Type = type;
            switch (type)
            {
                case WeaponType.Sword:
                    AttackBonus = 10;
                    Description = "A balanced weapon with good attack power";
                    MinLevel = 1;
                    break;
                case WeaponType.Spear:
                    AttackBonus = 12;
                    Description = "Long reach weapon with high attack power";
                    MinLevel = 1;
                    break;
                case WeaponType.Bow:
                    AttackBonus = 15;
                    Description = "Ranged weapon with high attack power";
                    MinLevel = 5;
                    break;
                case WeaponType.Axe:
                    AttackBonus = 20;
                    Description = "Heavy weapon with highest attack power";
                    MinLevel = 9;
                    break;
                case WeaponType.Hand:
                    AttackBonus = 0;
                    Description = "Just bare hands";
                    MinLevel = 0;
                    break;
                case WeaponType.ZeusThunderbolt:
                    if (achievementManager == null || !achievementManager.IsAchievementUnlocked("Collector"))
                    {
                        throw new InvalidOperationException("Zeus Thunderbolt is locked. Unlock the 'Collector' achievement first.");
                    }
                    AttackBonus = 999;
                    Description = "The weapon of the gods";
                    MinLevel = 0;
                    break;
            }
        }
    }
}
