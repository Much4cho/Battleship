using System.Collections.Immutable;

namespace Battleship
{
    public class Settings
    {
        public const int BoardSize = 10;
        public static readonly ImmutableArray<int> ships = ImmutableArray.Create(2, 3, 4);
    }
}
