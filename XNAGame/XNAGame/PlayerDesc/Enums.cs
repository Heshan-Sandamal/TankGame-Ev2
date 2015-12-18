using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNAGame.PlayerDesc
{
    public class Enums
    {
        public enum DamageLevel
        {
            NO_DAMAGE = 0,
            QUATER = 1,
            HALF = 2,
            CRITICAL = 3,
            TOTALED = 4
        };

        public enum Directions
        {
            NORTH = 0,
            EAST = 1,
            SOUTH = 2,
            WEST = 3
        };
        public enum Type
        {
            LIFE=0,
            BRICKS=1,
            STONE=2,
            WATER=3,
            COIN=4,
            PLAYER=5
        }
    }
}
