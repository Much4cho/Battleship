using System;
using System.Collections.Generic;

namespace Battleship
{
    public class ConsoleUi
    {
        public static void DrawBoard(IList<string> board, int offsetX, int offsetY)
        {
            for (int i = 0; i < board.Count; i++)
            {
                Console.SetCursorPosition(offsetX, i + offsetY);
                Console.Write(board[i]);
            }
        }
    }
}
