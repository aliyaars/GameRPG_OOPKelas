// See https://aka.ms/new-console-template for more information
using HerculesBattle.Managers;

namespace HerculesBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
        
    }
}