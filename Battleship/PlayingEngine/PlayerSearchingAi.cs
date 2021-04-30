using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.PlayingEngine
{
    // This inheritance is sketchy, probably better to have a abstract class for this
    public class PlayerSearchingAi : PlayerRandomAi, IPlayer
    {
        public PlayerSearchingAi(BattleshipBoard board) : base(board)
        {
            random = new Random();
            Board = board;
        }

        public override (int x, int y) MakeMove()
        {
            var hitNeighbors = Board.GetNeighbourTilesOfInjuredShips();
            (int x, int y) coords;

            if (hitNeighbors.Any())
            {
                coords = SearchingShot(hitNeighbors);
            }
            else
            {
                coords = RandomShot();
            }

            return coords;
        }

        protected (int x, int y) SearchingShot(IList<(int x, int y)> hitNeighbors)
        {
            var index = random.Next(hitNeighbors.Count);

            return hitNeighbors[index];
        }

        protected override (int x, int y) RandomShot()
        {
            var tiles = Board.GetAllOpenTiles();
            var index = random.Next(tiles.Count);
            var shootingCoords = tiles[index];

            // x & y both even or both odd searching pattern
            while (shootingCoords.x + shootingCoords.y % 2 == 0)
            {
                tiles.RemoveAt(index);
                index = random.Next(tiles.Count);
                shootingCoords = tiles[index];
            }

            return shootingCoords;
        }
    }
}
