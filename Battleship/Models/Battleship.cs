namespace Battleship.Models
{
    public class Battleship
    {
        public int Size { get; }
        public int Health { get; private set; }

        public Battleship(int size)
        {
            Size = size;
            Health = size;
        }

        public bool GetHit()
        {
            return --Health > 0;
        }

        public bool IsAlive => Health > 0;
    }

}
