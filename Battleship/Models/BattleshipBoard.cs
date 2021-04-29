using Battleship.Enums;
using Battleship.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Battleship.Models
{
    public class BattleshipBoard
    {
        readonly int boardSize = Settings.BoardSize;
        private IList<(int x, int y, int length, bool vertical)> shipsInformation;

        // Probably a mistake to use 2D Array cause me some bad blood
        // After a few tries of transforming this data into readable data
        // I Decided to change this into a collection of collection it's to stupid to work with
        // Typical overengineering simple things lol
        //public Battleship[,] MyBoard { get; private set; }
        // These 2 panels could be divided into 2 seperate classes
        public IReadOnlyList<IReadOnlyList<Tile>> DeffensePanel { get; }
        // This panel in theory is reduntant because we can have methods in the other's player Board that will let us see already shot tiles
        // I decided to go this route to keep it similar to the way the real game is played
        public IReadOnlyList<IReadOnlyList<OffenseOcupationTypeEnum>> OffensePanel { get; }
        public bool HasLost => !shipsInformation.Any(ship => DeffensePanel[ship.x][ship.y].Battleship?.IsAlive ?? false);


        public BattleshipBoard()
        {
            shipsInformation = new List<(int x, int y, int length, bool vertical)>();

            var rowBuilder = ImmutableArray.CreateBuilder<IReadOnlyList<Tile>>(boardSize);
            var Tilebuilder = ImmutableArray.CreateBuilder<Tile>();
            for (var i = 0; i < boardSize; i++)
            {
                Tilebuilder.Capacity = boardSize;
                for (var j = 0; j < boardSize; j++)
                {
                    Tilebuilder.Add(new Tile()); //Reference type
                }
                rowBuilder.Add(Tilebuilder.MoveToImmutable());
            }
            DeffensePanel = rowBuilder.MoveToImmutable();

            var rowOffenseBuilder = ImmutableArray.CreateBuilder<IReadOnlyList<OffenseOcupationTypeEnum>>(boardSize);
            for (var i = 0; i < boardSize; i++)
            {
                var row = new OffenseOcupationTypeEnum[boardSize];
                Array.Fill(row, OffenseOcupationTypeEnum.Blank); //Value type
                rowOffenseBuilder.Add(row.ToImmutableArray());
            }
            OffensePanel = rowOffenseBuilder.MoveToImmutable();

        }

        public void GetShot(int x, int y)
        {
            var possibleBattleship = DeffensePanel[x][y];

            if (!possibleBattleship.HasBattleship) return;
            if (possibleBattleship.HasBeenHit) throw new InvalidOperationException();

            var ship = possibleBattleship.Battleship;
            ship.GetHit();

            //if(ship.Health == 0)
            //{
            //    DeffensePanel.
            //}
        }

        // This might not belong here, as it is specific to the way it's displayed. Gonna leave it for now
        public IList<string> GetBoardAsListOfStringRows()
        {
            var result = new List<string>();

            result.Add("*** Offense Board ***");
            result.AddRange(OffensePanel
                            .Select(row =>
                                string.Join("",
                                    row.Select(tile =>
                                        tile.GetCharRepresentation())))
                            .ToList());
            result.Add("*** Deffense Board ***");
            result.AddRange(DeffensePanel
                            .Select(row =>
                                string.Join("",
                                    row.Select(tile =>
                                        tile.HasBattleship
                                        ? tile.Battleship.Health.ToString()
                                        : ".")))
                            .ToList());

            return result;
        }

        public IList<(int x, int y)> GetOpenTiles()
        {
            var result = new List<(int x, int y)>();

            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                {
                    if(OffensePanel[i][j] == OffenseOcupationTypeEnum.Blank) 
                        result.Add((i, j));
                }

            return result;
        }


        public bool TryAddBattleship(int x, int y, int length, bool vertical)
        {
            if (!vertical && x + length >= boardSize
                || vertical && y + length >= boardSize)
                throw new ArgumentOutOfRangeException();

            if (true)
            {

            }

            AddBattleship(x, y, length, vertical);

            return true;
        }

        private Battleship AddBattleship(int x, int y, int length, bool vertical)
        {
            var battleship = new Battleship(length);

            for (int i = 0; i < length; i++)
            {
                if (vertical) DeffensePanel[x][y+i].Battleship = battleship;
                if (!vertical) DeffensePanel[x+i][y].Battleship = battleship;
            }

            shipsInformation.Add((x, y, length, vertical));

            return battleship;
        }

    }

}
