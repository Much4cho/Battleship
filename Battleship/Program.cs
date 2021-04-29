using Battleship.Models;
using Battleship.PlayingEngine;
using System;
using System.Collections.Generic;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var board1 = new BattleshipBoard();
            var board2 = new BattleshipBoard();

            IShipPlacer shipPlacement = new ShipPlacementRandomizer();
            shipPlacement.PlaceShips(board1);
            shipPlacement.PlaceShips(board2);

            DrawBoard(board1.GetBoardAsListOfStringRows(), 0, 1);
            DrawBoard(board2.GetBoardAsListOfStringRows(), 30, 1);
        }

        static void DrawBoard(IList<string> board, int offsetX, int offsetY)
        {
            for (int i = 0; i < board.Count; i++)
            {
                Console.SetCursorPosition(offsetX, i+offsetY);
                Console.Write(board[i]);
            }
        }
    }
}
