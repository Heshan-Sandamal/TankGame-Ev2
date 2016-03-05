using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAGame.PlayerDesc
{
    class Graph
    {
        public static List<GameObject> vertexes;
        public static List<Edge> edges;

        public Graph(List<GameObject> vertexes, List<Edge> edges)
        {
            //Graph.vertexes = vertexes;
            //Graph.edges = edges;
        }
        public List<GameObject> getVertexes()
        {
            return vertexes;
        }

        public List<Edge> getEdges()
        {
            return edges;   
        }


    }
}
