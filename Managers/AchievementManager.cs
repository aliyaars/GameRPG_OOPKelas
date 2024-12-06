using System;
using System.Collections.Generic;
using HerculesBattle.Models;

namespace HerculesBattle.Managers
{
    public class AchievementManager
    {
        private List<Achievement> achievements;

        public AchievementManager()
        {
            achievements = new List<Achievement>
            {
                new Achievement("First Blood", "Defeat your first enemy."),
                new Achievement("Warrior", "Complete 5 levels."),
                new Achievement("Immortal", "Complete all the levels."),
                new Achievement("Master of Arms", "Use all weapon types."),
                new Achievement("Last Second Hero", "You won a battle with 1 HP left. Talk about cutting it close!"),
                new Achievement("Collector", "Whoa, you achieved all the achievements!"),
            };
        }

        public void UnlockAchievement(string name)
        {
            var achievement = achievements.Find(a => a.Name == name);
            if (achievement != null && !achievement.IsUnlocked)
            {
                achievement.Unlock();
                Console.WriteLine($"\nAchievement Unlocked: {achievement.Name} - {achievement.Description}");
            }
        }

        public void ShowAchievements()
        {
            Console.WriteLine("\n=== Achievements ===");
            foreach (var achievement in achievements)
            {
                Console.WriteLine(achievement);
            }
        }

        public bool IsAchievementUnlocked(string name)
        {
            var achievement = achievements.Find(a => a.Name == name);
            return achievement != null && achievement.IsUnlocked;
        }

        public void IsAllAchievementsUnlocked()
        {
            bool allUnlocked = true;
            foreach (var achievement in achievements)
            {
                if (!achievement.IsUnlocked && achievement.Name != "Collector")
                {
                    allUnlocked = false;
                    break;
                }
            }

            if (allUnlocked)
            {
                UnlockAchievement("Collector");
            }
        }
    }
}
