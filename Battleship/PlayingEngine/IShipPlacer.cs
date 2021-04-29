using Battleship.Models;

namespace Battleship.PlayingEngine
{
    public interface IShipPlacer
    {
        void PlaceShips(BattleshipBoard board);
    }
}
