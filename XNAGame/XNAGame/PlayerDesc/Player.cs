using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNAGame;

namespace XNAGame.PlayerDesc
{
    class Player:GameObject
    {
        
        public Enums.Directions Direction { get; set; }
        public int Shot { get; set; }
        public int health { get; set; }
        public int Coins { get; set; }
        public int points { get; set; }
        public int Number { get; set; }
        public int playerType { get; set; }


    }
}
