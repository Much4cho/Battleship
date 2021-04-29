using Battleship.Enums;
using System;

namespace Battleship.Utility
{
    public static class EnumExtensions
    {
        public static char GetCharRepresentation(this OffenseOcupationTypeEnum enumerationValue) 
        {
            switch (enumerationValue)
            {
                case OffenseOcupationTypeEnum.Blank:    return '.';
                case OffenseOcupationTypeEnum.Hit:      return 'X';
                case OffenseOcupationTypeEnum.Missed:   return 'O';

                default: throw new ArgumentException();
            }
        }
    }
}
