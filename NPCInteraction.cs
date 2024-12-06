using HerculesBattle.Models;

namespace HerculesBattle
{
    public class NPCInteraction
    {
        public void TestNPCInteraction()
        {
            Character player = new Character("Hercules");

            NPC wiseOldMan = new NPC(
                "Wise Old Man",
                "Mentor",
                new Dictionary<string, (string, Action<Character>)>
                {
                    { "1", ("Tell me about the battle strategies.", player => {
                        Console.WriteLine("Wise Old Man: Always strike first and stay on your guard.");
                        player.AdjustStats(0, 0, 5);
                    }) },
                    { "2", ("Can you heal me?", player => {
                        Console.WriteLine("Wise Old Man: Let me restore your health, young warrior.");
                        player.AdjustStats(20, 0, 0);
                    }) },
                    { "3", ("Leave me alone.", player => {
                        Console.WriteLine("Wise Old Man: How rude! You lose some energy.");
                        player.AdjustStats(0, -10, 0);
                    }) }
                }
            );

            wiseOldMan.StartDialogue(player);
        }
    }
}
