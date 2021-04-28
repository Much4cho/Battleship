using Battleship.Models;
using System;
using System.Collections.Generic;

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


            DrawBoard(board1.GetBoardAsListOfStringRows(), 0, 1);
            //DrawBoard(board1, 0, 1);
            //DrawBoard(board2, 30, 1);
        }

        static void DrawBoard(IList<string> board, int offsetX, int offsetY)
        {
            for (int i = 0; i < Settings.BoardSize; i++)
            {
                Console.SetCursorPosition(offsetX, i+offsetY);
                Console.Write(board[i]);
            }
        }
    }
}
