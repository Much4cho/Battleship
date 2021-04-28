namespace Battleship.Models
{
    // Decided to create this as a class, to store more information or be able to have different kinds of action/results kept
    public class Tile
    {
        public bool HasBeenHit { get; set; }
        public Battleship Battleship { get; set; }
        public bool HasBattleship => Battleship != null;
    }
}
