using Battleship.Models;
using System;

namespace Battleship.PlayingEngine
{
    public class PlayerRandomAi : IPlayer
    {
        // Maybe singleton for this is valid
        protected Random random;
        public virtual BattleshipBoard Board { get; set; }

        public PlayerRandomAi(BattleshipBoard board)
        {
            random = new Random();
            Board = board;
        }

        public virtual (int x, int y) MakeMove()
        {
            var coords = RandomShot();

            return coords;
        }

        protected virtual (int x, int y) RandomShot()
        {
            // A this point it's thinking about is it better to have redundancy and keep it in this class and update every time
            // or just transform the tiles every move. Leaning towards first option, but can see arguments both ways. (And no time)
            var tiles = Board.GetAllOpenTiles();
            var index = random.Next(tiles.Count);

            return tiles[index];
        }
    }
}
