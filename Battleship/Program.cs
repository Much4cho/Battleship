using Battleship.Models;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetCursorPosition(20, 0);
            Console.WriteLine("Hello");
            Console.SetCursorPosition(20, 2);
            Console.WriteLine("World");
            Console.ReadLine();

            var board1 = new BattleshipBoard();
            var board2 = new BattleshipBoard();
        }
    }
}
