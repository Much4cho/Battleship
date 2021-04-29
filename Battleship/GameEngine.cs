using Battleship.Enums;
using Battleship.Models;
using Battleship.PlayingEngine;
using System;
using System.Threading;

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

            ConsoleUi.DrawBoard(board1.GetBoardAsListOfStringRows(), 0, 1);
            ConsoleUi.DrawBoard(board2.GetBoardAsListOfStringRows(), 35, 1);
        }

        public void Play()
        {
            while (!board1.HasLost && !board2.HasLost)
            {
                var shootingCoord = player1.MakeMove();
                var shootingResult = board2.GetShot(shootingCoord.x, shootingCoord.y);
                board1.OffensePanel[shootingCoord.x][shootingCoord.y]
                    = shootingResult == ShootingResultEnum.Missed
                        ? OffenseOcupationTypeEnum.Missed
                        : OffenseOcupationTypeEnum.Hit;

                ConsoleUi.DrawBoard(board1.GetBoardAsListOfStringRows(), 0, 1);

                if (board2.HasLost) break;

                shootingCoord = player2.MakeMove();
                shootingResult = board1.GetShot(shootingCoord.x, shootingCoord.y);
                board2.OffensePanel[shootingCoord.x][shootingCoord.y]
                    = shootingResult == ShootingResultEnum.Missed
                        ? OffenseOcupationTypeEnum.Missed
                        : OffenseOcupationTypeEnum.Hit;


                ConsoleUi.DrawBoard(board2.GetBoardAsListOfStringRows(), 35, 1);

                //Thread.Sleep(500);
                Console.ReadKey();
            }

            ConsoleUi.ShowWinner(board1.HasLost ? "Player 2 Won": "Player 1 Won");
        }
    }
}
