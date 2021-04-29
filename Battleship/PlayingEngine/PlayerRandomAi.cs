using Battleship.Models;

namespace Battleship.PlayingEngine
{
    public class PlayerRandomAi : IPlayer
    {
        public BattleshipBoard Board { get; set; }
        public PlayerRandomAi()
        {
            Board = new BattleshipBoard();
        }

        public PlayerRandomAi(BattleshipBoard board)
        {
            Board = board;
        }

        public void MakeMove()
        {
            // TODO
        }
    }
}
