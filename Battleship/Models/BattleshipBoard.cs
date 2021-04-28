using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Battleship.Models
{
    public class BattleshipBoard
    {
        readonly int boardSize = Settings.BoardSize;

        public int MyProperty { get; set; }

        // Probably a mistake to use 2D Array cause me some bad blood
        // After a few tries of transforming this data into readable data
        // I Decided to change this into a collection of collection it's to stupid to work with
        // Typical overengineering simple things lol
        //public Battleship[,] MyBoard { get; private set; }
        public IReadOnlyList<IReadOnlyList<Tile>> DeffensePanel { get; set; }
        public IReadOnlyList<char[]> OffensePanel { get; set; }

        public BattleshipBoard()
        {
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
            DeffensePanel = rowBuilder.MoveToImmutable();

            var rowOffenseBuilder = ImmutableArray.CreateBuilder<char[]>(boardSize);
            for (var i = 0; i < boardSize; i++)
            {
                var row = new char[boardSize];
                Array.Fill(row, '.');
                rowOffenseBuilder.Add(row);
            }
            OffensePanel = rowOffenseBuilder.MoveToImmutable();

            AddBattleship(2, 3, 3, true);
            AddBattleship(5, 5, 4, false);
        }

        public void Shoot(int x, int y)
        {
            var possibleBattleship = DeffensePanel[x][y];

            if (!possibleBattleship.HasBattleship) return;

            possibleBattleship.Battleship.GetHit();

            // scan
        }

        public IList<string> GetBoardAsListOfStringRows()
        {
            return DeffensePanel
                .Select(row => 
                    string.Join("", 
                        row.Select(tile => 
                            tile.HasBattleship 
                            ? tile.Battleship.Health.ToString()
                            : ".")))
                .ToList();
        }

        private Battleship AddBattleship(int x, int y, int length, bool vertical)
        {
            if (!vertical && x + length >= boardSize
                || vertical && y + length >= boardSize)
                throw new ArgumentOutOfRangeException();

            var battleship = new Battleship(length);

            for (int i = 0; i < length; i++)
            {
                if (vertical) DeffensePanel[x][y+i].Battleship = battleship;
                if (!vertical) DeffensePanel[x+i][y].Battleship = battleship;
            }

            return battleship;
        }

    }

}
