namespace HerculesBattle.Core
{
    public class GameSettings
    {
        public enum DifficultyLevel
        {
            Normal,
            Hardcore
        }

        public DifficultyLevel Difficulty { get; set; }
        public int BattleSpeed { get; set; }
        public bool SoundEnabled { get; set; }

        public GameSettings()
        {
            Difficulty = DifficultyLevel.Normal;
            BattleSpeed = 1;
            SoundEnabled = true;
        }

        public void AdjustDifficulty(DifficultyLevel newDifficulty)
        {
            Difficulty = newDifficulty;
            // Implement difficulty-specific adjustments here
        }

        public void SetBattleSpeed(int speed)
        {
            BattleSpeed = Math.Clamp(speed, 1, 3);
        }

        public void ToggleSound()
        {
            SoundEnabled = !SoundEnabled;
        }
    }
}