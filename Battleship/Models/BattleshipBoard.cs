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
        private ISet<(int x, int y)> coordsOfTilesThatCantHaveShips;

        // Probably a mistake to use 2D Array caused me some bad blood
        // After a few tries of transforming this data into readable data
        // I Decided to change this into a collection of collection it's too stupid to work with
        // Typical overengineering simple things lol
        // public Battleship[,] MyBoard { get; private set; }

        // These 2 panels could be divided into 2 seperate classes
        public IReadOnlyList<IReadOnlyList<Tile>> DeffensePanel { get; }
        // This panel in theory is reduntant because we can have methods in the other's player Board that will let us see already shot tiles
        // I decided to go this route to keep it similar to the way the real game is played
        public IReadOnlyList<IList<OffenseOcupationTypeEnum>> OffensePanel { get; }
        public bool HasLost => !shipsInformation.Any(ship => DeffensePanel[ship.x][ship.y].Battleship?.IsAlive ?? false);


        public BattleshipBoard()
        {
            coordsOfTilesThatCantHaveShips = new HashSet<(int, int)>();
            shipsInformation = new List<(int x, int y, int length, bool vertical)>();

            var deffenseRowBuilder = ImmutableArray.CreateBuilder<IReadOnlyList<Tile>>(boardSize);
            var deffenseTilebuilder = ImmutableArray.CreateBuilder<Tile>();
            for (var i = 0; i < boardSize; i++)
            {
                deffenseTilebuilder.Capacity = boardSize;
                for (var j = 0; j < boardSize; j++)
                {
                    deffenseTilebuilder.Add(new Tile());
                }
                deffenseRowBuilder.Add(deffenseTilebuilder.MoveToImmutable());
            }
            DeffensePanel = deffenseRowBuilder.MoveToImmutable();

            var offenseRowBuilder = ImmutableArray.CreateBuilder<IList<OffenseOcupationTypeEnum>>(boardSize);
            for (var i = 0; i < boardSize; i++)
            {
                var row = new OffenseOcupationTypeEnum[boardSize];
                Array.Fill(row, OffenseOcupationTypeEnum.Blank);
                offenseRowBuilder.Add(row);
            }
            OffensePanel = offenseRowBuilder.MoveToImmutable();
        }

        // This might not belong here, as it is specific to the way it's displayed. Gonna leave it for now
        public IList<string> GetBoardAsListOfStringRows()
        {
            var result = new List<string>();

            var offenseStringRows = OffensePanel
                            .Select(row =>
                                string.Join("", row.Select(tile =>
                                        tile.GetCharRepresentation())));

            var deffenseStringRows = DeffensePanel
                            .Select(row =>
                                string.Join("", row.Select(tile =>
                                    tile.HasBattleship
                                    ? tile.Battleship.Health.ToString()
                                    : ".")));

            result.Add("*** Offense Board ***");
            result.AddRange(offenseStringRows);
            result.Add("*** Deffense Board ***");
            result.AddRange(deffenseStringRows);

            return result;
        }

        public IList<(int x, int y)> GetAllOpenTiles()
        {
            var result = new List<(int x, int y)>();

            for (int i = 0; i < boardSize; i++)
            for (int j = 0; j < boardSize; j++)
            {
                if (OffensePanel[i][j] == OffenseOcupationTypeEnum.Blank)
                    result.Add((i, j));
            }

            return result;
        }

        public IList<(int x, int y)> GetNeighbourTilesOfInjuredShips()
        {
            var result = new List<(int x, int y)>();

            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                {
                    if (OffensePanel[i][j] == OffenseOcupationTypeEnum.Hit)
                    {
                        var neigbours = new List<(int x, int y)>(4);
                    
                        if (i != 0)             neigbours.Add((i - 1, j));
                        if (j != 0)             neigbours.Add((i, j - 1));
                        if (i != boardSize - 1) neigbours.Add((i + 1, j));
                        if (j != boardSize - 1) neigbours.Add((i, j + 1));

                        var blankNeighbours = neigbours.Where(coord => 
                                OffensePanel[coord.x][coord.y] == OffenseOcupationTypeEnum.Blank);
                        result.AddRange(blankNeighbours);
                    }
                }

            return result.Distinct().ToList();
        }

        public ShootingResultEnum GetShot(int x, int y)
        {
            var possibleBattleship = DeffensePanel[x][y];

            if (!possibleBattleship.HasBattleship) return ShootingResultEnum.Missed;
            if (possibleBattleship.HasBeenHit) throw new InvalidOperationException();

            var ship = possibleBattleship.Battleship;
            ship.GetHit();

            return ship.IsAlive
                ? ShootingResultEnum.Hit
                : ShootingResultEnum.Destroyed;
        }

        public bool TryAddBattleship(int x, int y, int length, bool vertical)
        {
            if (!vertical && x + length >= boardSize
                || vertical && y + length >= boardSize)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < length; i++)
            {
                if (vertical && coordsOfTilesThatCantHaveShips.Contains((x,y + i))) return false;
                if (!vertical && coordsOfTilesThatCantHaveShips.Contains((x + i, y))) return false;
            }

            AddBattleship(x, y, length, vertical);

            return true;
        }

        public bool TryAddBattleshipAllowNeighbours(int x, int y, int length, bool vertical)
        {
            if (!vertical && x + length >= boardSize
                || vertical && y + length >= boardSize)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < length; i++)
            {
                if(vertical && DeffensePanel[x][y+i].HasBattleship) return false;
                if(!vertical && DeffensePanel[x+i][y].HasBattleship) return false;
            }

            AddBattleship(x, y, length, vertical);

            return true;
        }

        private Battleship AddBattleship(int x, int y, int length, bool vertical)
        {
            var battleship = new Battleship(length);

            var xTemp = x;
            var yTemp = y;
            for (int i = 0; i < length; i++)
            {
                DeffensePanel[xTemp][yTemp].Battleship = battleship;

                coordsOfTilesThatCantHaveShips.Add((xTemp, yTemp));
                if (xTemp != 0) coordsOfTilesThatCantHaveShips.Add((xTemp - 1, yTemp));
                if (yTemp != 0) coordsOfTilesThatCantHaveShips.Add((xTemp, yTemp - 1));
                if (xTemp != boardSize - 1) coordsOfTilesThatCantHaveShips.Add((xTemp + 1, yTemp));
                if (yTemp != boardSize - 1) coordsOfTilesThatCantHaveShips.Add((xTemp, yTemp + 1));

                if (vertical) yTemp++;
                else xTemp++;
            }

            shipsInformation.Add((x, y, length, vertical));

            return battleship;
        }

    }
}
