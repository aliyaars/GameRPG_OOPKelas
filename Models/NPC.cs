using System;
using System.Collections.Generic;

namespace HerculesBattle.Models
{
    public class NPC
    {
        public string Name { get; private set; }
        public string Role { get; private set; }
        public Dictionary<string, (string, Action<Character>)> DialogueOptions { get; private set; }

        public NPC(string name, string role, Dictionary<string, (string, Action<Character>)> dialogueOptions)
        {
            Name = name;
            Role = role;
            DialogueOptions = dialogueOptions;
        }

        public void StartDialogue(Character player)
        {
            Console.WriteLine($"\nYou are talking to {Name} ({Role}):");

            foreach (var option in DialogueOptions)
            {
                Console.WriteLine($"{option.Key}. {option.Value.Item1}");
            }

            string choice = string.Empty;
            while (!DialogueOptions.ContainsKey(choice))
            {
                Console.Write("\nChoose your response: ");
                choice = Console.ReadLine() ?? string.Empty;

                if (!DialogueOptions.ContainsKey(choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }

            Console.WriteLine($"\n{Name}: {DialogueOptions[choice].Item1}");
            DialogueOptions[choice].Item2(player);

            Console.WriteLine("\nInteraction complete!");
        }
    }
}
