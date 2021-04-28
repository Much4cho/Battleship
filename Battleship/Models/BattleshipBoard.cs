using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Models
{
    public class BattleshipBoard
    {
        readonly int boardSize = Settings.BoardSize;

        public char[,] ShootingBoard { get; private set; }
        public Battleship[,] MyBoard { get; private set; }
        public BattleshipBoard()
        {
            ShootingBoard = new char[boardSize, boardSize];
            MyBoard = new Battleship[boardSize, boardSize];

            for (var i = 0; i < boardSize; i++)
            for (var j = 0; j < boardSize; j++)
            {
                ShootingBoard[i, j] = '.';
            }

            AddBattleship(2, 3, 3, true);
            AddBattleship(5, 5, 4, false);
        }

        public void Shoot(int x, int y)
        {
            var possibleBattleship = MyBoard[x, y];

            if (possibleBattleship == null) return;

            possibleBattleship.GetHit();

            // scan
        }

        public string[] GetReadableBoard()
        {
            //var result = new string[boardSize];

            //Buffer.BlockCopy(MyBoard, 0, result, 0, 10);

            //var result2 =   (from Battleship tile in MyBoard
            //                select tile != null ? 'O' : '.');
            //Array
            //MyBoard.Cop
            //var result = MyBoard.Batch<Battleship>(boardSize);

            //return result;
            return new string[1];
        }

        private Battleship AddBattleship(int x, int y, int length, bool vertical)
        {
            if (!vertical && x + length >= boardSize
                || vertical && y + length >= boardSize)
                throw new ArgumentOutOfRangeException();

            var battleship = new Battleship(length);

            for (int i = 0; i < length; i++)
            {
                if (vertical) MyBoard[x, y+i] = battleship;
                if (!vertical) MyBoard[x+i, y] = battleship;
            }

            return battleship;
        }

    }



}
