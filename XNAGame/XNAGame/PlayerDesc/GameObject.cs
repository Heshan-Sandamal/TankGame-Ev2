using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAGame.PlayerDesc
{
    public class GameObject
    {
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public Enums.Type Type { get; set; }
        public int id { get; set; }
            
        public override int GetHashCode()
        {
            
              int prime = 31;
              int result = 1;
              result = prime * result + ((id == 0) ? 0 : id.GetHashCode());
              return result;
        }

        public override bool Equals(object obj)
        {
            if (this.ToString() == obj.ToString())
                return true;
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            GameObject other = (GameObject)obj;
            if (id == null)
            {
                if (other.id != null)
                    return false;
            }
            else if (!id.Equals(other.id))
                return false;
            return true;
        }
        public override string ToString()
        {
            return LocationX+":"+LocationY;
        }
    }
}
