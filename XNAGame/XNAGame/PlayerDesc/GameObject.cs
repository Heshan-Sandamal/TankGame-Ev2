using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAGame.PlayerDesc
{
    class GameObject
    {
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public int Damage { get; set; }
        public Enums.Type Type { get; set; }
    }
}
