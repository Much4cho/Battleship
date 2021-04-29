using Battleship.PlayingEngine;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new GameEngine();

            engine.PlaceShips();
            Console.ReadKey();

            engine.Play();
            Console.ReadKey();
        }
    }
}
