using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAGame.PlayerDesc
{
    class Edge
    {
        public int id { get; set; }
        public GameObject source { get; set; }
        public GameObject destination { get; set; }
        public int cost { get; set; }
        public Edge(int id,GameObject source,GameObject destination,int cost) {
            this.id = id;
            this.source = source;
            this.destination = destination;
            this.cost = cost;
        }
        public String toString()
        {
            return source.ToString() + "" + destination.ToString();
        }
    }
}
