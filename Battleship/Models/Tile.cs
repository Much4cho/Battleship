namespace Battleship.Models
{
    public class Tile
    {
        public bool HasBeenHit { get; set; }
        public Battleship Battleship { get; set; }
        public bool HasBattleship => Battleship != null;
    }
}
