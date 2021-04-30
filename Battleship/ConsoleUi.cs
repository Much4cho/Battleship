using System;
using System.Collections.Generic;
using System.Threading;

namespace Battleship
{
    public static class ConsoleUi
    {
        private static int horizontalStep = 3;
        private static int verticalStep = 1;
        private static int movingTextSize = 30;
        private static int movingTextBreakSize = 5;

        public static void DrawBoard(IList<string> board, int offsetX, int offsetY)
        {
            for (int i = 0; i < board.Count; i++)
            {
                Console.SetCursorPosition(offsetX, i + offsetY);
                Console.Write(board[i]);
            }
        }

        public static void ShowWinner(string text)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(text);
        }

        public static void MovingScreen()
        {
            var animationOrder = new[] { 
                "*****",
                "     ",
                "#####",
                "*****",
                "     ",
                "     ",
                "     "
            };

            for (var i = 0; i < movingTextSize + movingTextBreakSize * animationOrder.Length; i++)
            {
                for (int j = 0; j < animationOrder.Length; j++)
                {
                    if (i >= movingTextBreakSize * j
                        && i < movingTextSize + movingTextBreakSize * j)
                    {
                        MovingScreenHelper(animationOrder[j], i - movingTextBreakSize * j);

                    }
                }

                Thread.Sleep(40);
            }

            Console.Clear();
        }

        private static void MovingScreenHelper(string text, int i)
        {
            Console.SetCursorPosition(i * horizontalStep, i * verticalStep);
            Console.Write(text);

            for (var j = i; j >= 0; j--)
            {
                Console.SetCursorPosition(i * horizontalStep, j * verticalStep);
                Console.Write(text);

                Console.SetCursorPosition(j * horizontalStep, i * verticalStep);
                Console.Write(text);
            }
        }
    }
}
