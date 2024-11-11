namespace HerculesBattle.Systems
{
    public class Battle
    {
        private Character player;
        private Enemy enemy;
        private bool isPlayerTurn;

        public Battle(Character player, Enemy enemy)
        {
            this.player = player;
            this.enemy = enemy;
            isPlayerTurn = player.Speed >= enemy.Speed;
        }

        public void Start()
        {
            Console.WriteLine($"Battle starts! {player.Name} vs {enemy.Name}");
        
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
            }

            EndBattle();
        }

        private void PlayerTurn()
        {
            Console.WriteLine("\nPlayer's turn!");
            Console.WriteLine($"Health: {player.Health}/{player.MaxHealth}");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Defend");
            Console.WriteLine("3. Use Item");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    PerformAttack(player.AttackPower, enemy);
                    break;
                case "2":
                    // Implement defend logic
                    break;
                case "3":
                    // Implement item usage
                    break;
            }
        }

        private void EnemyTurn()
        {
            Console.WriteLine("\nEnemy's turn!");
            PerformAttack(enemy.AttackPower, player);
        }

        private void PerformAttack(int attackPower, object target)
        {
            if (target is Character character)
            {
                character.TakeDamage(attackPower);
                Console.WriteLine($"Enemy deals {attackPower} damage to {character.Name}!");
            }
            else if (target is Enemy enemy)
            {
                enemy.TakeDamage(attackPower);
                Console.WriteLine($"{player.Name} deals {attackPower} damage to {enemy.Name}!");
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