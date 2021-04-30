using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.PlayingEngine
{
    public class ShipPlacementRandomizer : IShipPlacer
    {
        private IList<(int x, int y, int length, bool vertical)> shipsSoFar;

        // TODO check if ships exists already
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

                (int x, int y, int length, bool vertical) ship = default;

                var addingWasSuccesfull = false;
                while (!addingWasSuccesfull)
                {
                    var vertical = random.Next(2) == 1;
                    var maxX = boardSize;
                    var maxY = boardSize;
                    if (!vertical) maxX -= shipLength;
                    if (vertical) maxY -= shipLength;

                    ship = (
                    random.Next(maxX),
                    random.Next(maxY),
                    shipLength,
                    vertical);

                    addingWasSuccesfull = board.TryAddBattleshipAllowNeighbours(ship.x, ship.y, ship.length, ship.vertical);
                }

                shipsSoFar.Add(ship);
                shipLenghts.RemoveAt(shipIndex);
            }
        }
    }
}
