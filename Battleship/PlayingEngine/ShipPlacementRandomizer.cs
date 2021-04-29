using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.PlayingEngine
{
    public class ShipPlacementRandomizer : IShipPlacer
    {
        private IList<(int x, int y, int length, bool vertical)> shipsSoFar;

        public void PlaceShips(BattleshipBoard board)
        {
            shipsSoFar = new List<(int x, int y, int length, bool vertical)>();

            var random = new Random();
            var boardSize = Settings.BoardSize;
            var shipLenghts = Settings.ships.ToList();

            while (shipLenghts.Any())
            {
                var shipIndex = random.Next(shipLenghts.Count);

                var shipLength = shipLenghts[shipIndex];
                var vertical = random.Next(2) == 1; // Is it better to random % 2, then e.g. random(2)?? I would assume no, because .net handles it already hopefully
                var maxX = boardSize;
                var maxY = boardSize;
                if (!vertical) maxX = maxX - shipLength;
                if (vertical)  maxY = maxY - shipLength;

                (int x, int y, int length, bool vertical) ship = (
                    random.Next(maxX),
                    random.Next(maxY),
                    shipLength,
                    vertical); 

                var addingWasSuccesfull = board.TryAddBattleship(ship.x, ship.y, ship.length, ship.vertical);
                if(addingWasSuccesfull)
                {
                    shipsSoFar.Add(ship);
                    shipLenghts.RemoveAt(shipIndex);
                }
            }
        }
    }
}
