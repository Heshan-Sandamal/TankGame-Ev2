using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNAGame;

namespace XNAGame.PlayerDesc
{
    class Player:Coordinate
    {
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public Enums.Directions Direction { get; set; }
        public int Shot { get; set; }
        public int health { get; set; }
        public int Coins { get; set; }
        public int points { get; set; }
        public int Number { get; set; }


    }
}
