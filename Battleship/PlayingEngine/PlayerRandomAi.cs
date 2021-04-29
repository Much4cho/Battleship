using Battleship.Models;
using System;

namespace Battleship.PlayingEngine
{
    public class PlayerRandomAi : IPlayer
    {
        // Maybe singleton for this is valid
        private Random random;
        public BattleshipBoard Board { get; set; }
        public PlayerRandomAi()
        {
            random = new Random();
            Board = new BattleshipBoard();
        }

        public PlayerRandomAi(BattleshipBoard board)
        {
            random = new Random();
            Board = board;
        }

        public (int x, int y) MakeMove()
        {
            //var hitNeighbors = Board.GetHitNeighbors();
            (int x, int y) coords;
            //if (hitNeighbors.Any())
            //{
            //    coords = SearchingShot();
            //}
            //else
            //{
            //    coords = RandomShot();
            //}
            coords = RandomShot();

            //Board.GetShot(coords.x, coords.y);

            return coords;
        }

        private (int x, int y) SearchingShot()
        {
            return (1, 2);
        }

        private (int x, int y) RandomShot()
        {
            // A this point it's thinking about is it better to have redundancy and keep it in this class and update every time
            // or just transform the tiles every move. Leaning towards first option, but can see arguments both ways. (And no time)
            var tiles = Board.GetOpenTiles();
            var index = random.Next(tiles.Count);

            return tiles[index];
        }
    }
}
