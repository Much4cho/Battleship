using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Battleship.Models
{
    class MistakesHappen
    {
        // Bad choice because map should be able to manage itself and needs to know where ships are on the map
        public class BattleshipBadChoice
        {
            public List<Coord> Coords { get; } = new List<Coord>();

            public BattleshipBadChoice(Coord starting, int length, bool vertical)
            {
                if (!vertical && starting.X + length >= Settings.BoardSize
                    || vertical && starting.Y + length >= Settings.BoardSize)
                    throw new ArgumentOutOfRangeException();

                for (int i = 0; i < length; i++)
                {
                    if (vertical) Coords.Add(new Coord(starting.X, starting.Y + i));
                    if (!vertical) Coords.Add(new Coord(starting.X + i, starting.Y));
                }
            }

            public BattleshipBadChoice(Coord starting, Coord ending)
            {
                if (starting.X >= Settings.BoardSize
                    || starting.Y >= Settings.BoardSize
                    || ending.X >= Settings.BoardSize
                    || ending.Y >= Settings.BoardSize) throw new ArgumentOutOfRangeException();

                if (starting.X == ending.X)
                {

                }

                if (starting.Y == ending.Y)
                {

                }

                throw new ArgumentException();
            }

            // Went different Route, dont need it
            public class Coord
            {
                public int X { get; }
                public int Y { get; }

                public Coord(int x, int y)
                {
                    X = x;
                    Y = y;
                }
            }

            private void TheseBuilderWereUnnecesary()
            {
                var boardSize = Settings.BoardSize;
                var rowBuilder = ImmutableArray.CreateBuilder<IReadOnlyList<Tile>>(boardSize);
                var Tilebuilder = ImmutableArray.CreateBuilder<Tile>();
                for (var i = 0; i < boardSize; i++)
                {
                    Tilebuilder.Capacity = boardSize;
                    for (var j = 0; j < boardSize; j++)
                    {
                        Tilebuilder.Add(new Tile());
                    }
                    rowBuilder.Add(Tilebuilder.MoveToImmutable());
                }
                //DeffensePanel = rowBuilder.MoveToImmutable();

                var rowOffenseBuilder = ImmutableArray.CreateBuilder<char[]>(boardSize);
                for (var i = 0; i < boardSize; i++)
                {
                    var row = new char[boardSize];
                    Array.Fill(row, '.');
                    rowOffenseBuilder.Add(row);
                }
                //OffensePanel = rowOffenseBuilder.MoveToImmutable();
            }
        }
    }
}
