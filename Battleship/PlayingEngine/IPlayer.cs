namespace Battleship.PlayingEngine
{
    public interface IPlayer
    {
        (int x, int y) MakeMove();
        //void MakeMove();
    }
}
