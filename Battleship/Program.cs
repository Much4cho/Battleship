using Battleship.PlayingEngine;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new GameEngine();

            Console.SetWindowSize(90, 30);
            Console.SetBufferSize(90, 30);

            ConsoleUi.MovingScreen();

            engine.PlaceShips();
            Console.ReadKey();

            engine.Play();
            Console.ReadKey();
        }
    }
}
