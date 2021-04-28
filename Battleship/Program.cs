using Battleship.Models;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetCursorPosition(20, 0);
            //Console.WriteLine("Hello");
            //Console.SetCursorPosition(20, 2);
            //Console.WriteLine("World");
            //Console.ReadLine();

            var board1 = new BattleshipBoard();
            var board2 = new BattleshipBoard();

            board1.GetReadableBoard();

            //DrawBoard(board1, 0, 1);
            //DrawBoard(board2, 30, 1);
        }

        static void DrawBoard(BattleshipBoard board, int offsetX, int offsetY)
        {
            // I don't know how fast this is, but my time and readability-wise it's good enough
            for (int i = 0; i < Settings.BoardSize; i++)
            for (int j = 0; j < Settings.BoardSize; j++)
            {
                Console.SetCursorPosition(i+offsetX, j+ offsetY);
                //Console.Write(board.Board[i,j]);
            }
        }
    }
}
