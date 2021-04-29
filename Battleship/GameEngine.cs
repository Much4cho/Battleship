using Battleship.Models;
using Battleship.PlayingEngine;

namespace Battleship
{
    public class GameEngine
    {
        BattleshipBoard board1;
        BattleshipBoard board2;

        public IPlayer player1;
        public IPlayer player2;

        public GameEngine()
        {
            board1 = new BattleshipBoard();
            board2 = new BattleshipBoard();

            player1 = new PlayerRandomAi(board1);
            player2 = new PlayerRandomAi(board2);
        }

        public void PlaceShips()
        {
            IShipPlacer shipPlacement = new ShipPlacementRandomizer();
            shipPlacement.PlaceShips(board1);
            shipPlacement.PlaceShips(board2);

            ConsoleUi.DrawBoard(board1.GetBoardAsListOfStringRows(), 0, 0);
            ConsoleUi.DrawBoard(board2.GetBoardAsListOfStringRows(), 35, 0);
        }

        public void Play()
        {
            

            
        }
    }
}
