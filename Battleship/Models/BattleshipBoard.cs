using System;
using System.Collections.Generic;

namespace Battleship.Models
{
    public class BattleshipBoard
    {
        private List<Battleship> battleships;
        string[,] board;

        public BattleshipBoard()
        {
            board = new string[Settings.BoardSize, Settings.BoardSize];

            battleships = new List<Battleship>();
            battleships.Add(new Battleship(new Coord(2, 3), new Coord(2, 5)));
        }
    }

    public class Battleship
    {
        private List<Coord> coords;

        public Battleship(Coord starting, Coord ending)
        {
            if (starting.X >= Settings.BoardSize
                || starting.Y >= Settings.BoardSize
                || ending.X >= Settings.BoardSize
                || ending.Y >= Settings.BoardSize) throw new ArgumentOutOfRangeException();

            if(starting.X == ending.X)
            {

            }

            if (starting.Y == ending.Y)
            {

            }

            throw new ArgumentException();
        }
    }

    public class Coord
    {
        public int X { get;}
        public int Y { get; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
