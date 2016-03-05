using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAGame.PlayerDesc.AI
{
    class DijkstraAlgorithm
    {
        private readonly List<GameObject> nodes;
        private readonly List<Edge> edges;
        private ISet<GameObject> settledNodes;
        private ISet<GameObject> unSettledNodes;
        private Dictionary<GameObject, GameObject> predecessors;
        private Dictionary<GameObject, int> distance;

        public DijkstraAlgorithm(Graph graph)
        {
            // create a copy of the array so that we can operate on this array
            this.nodes = new List<GameObject>(graph.getVertexes());
            this.edges = new List<Edge>(graph.getEdges());
        }
        public void execute(GameObject source)
        {
            settledNodes = new HashSet<GameObject>();
            unSettledNodes = new HashSet<GameObject>();
            distance = new Dictionary<GameObject, int>();
            predecessors = new Dictionary<GameObject, GameObject>();
            distance.Add(source, 0);
            unSettledNodes.Add(source);
            while (unSettledNodes.Count > 0)
            {
                GameObject node = getMinimum(unSettledNodes);
                settledNodes.Add(node);
                unSettledNodes.Remove(node);
                findMinimalDistances(node);
            }
        }

        private GameObject getMinimum(ISet<GameObject> vertexes) {
                GameObject minimum = null;
                foreach (GameObject gameObj in vertexes) {
                  if (minimum == null) {
                      minimum = gameObj;
                  } else {
                      if (getShortestDistance(gameObj) < getShortestDistance(minimum))
                      {
                          minimum = gameObj;
                    }
                  }
                }
                return minimum;
        }
        private void findMinimalDistances(GameObject node)
        {
            List<GameObject> adjacentNodes = getNeighbors(node);
            foreach (GameObject target in adjacentNodes)
            {
                if (getShortestDistance(target) > getShortestDistance(node)
                    + getDistance(node, target))
                {
                    distance.Add(target, getShortestDistance(node)
                        + getDistance(node, target));
                    predecessors.Add(target, node);
                    unSettledNodes.Add(target);
                }
            }
        }
        private int getDistance(GameObject node, GameObject target)
        {
            foreach (Edge edge in edges)
            {
                if (edge.source.Equals(node) && edge.destination.Equals(target))
                {
                    return edge.cost;
                }
            }
            throw new Exception("Something went wrong");
        }
        private List<GameObject> getNeighbors(GameObject node) {
            List<GameObject> neighbors = new List<GameObject>();
            foreach (Edge edge in edges) {
              if (edge.source.Equals(node) && !isSettled(edge.destination)) {
                neighbors.Add(edge.destination);
              }
            }
            return neighbors;
        }
        private Boolean isSettled(GameObject vertex)
        {
            return settledNodes.Contains(vertex);
        }
        private int getShortestDistance(GameObject destination)
        {
            int d;
            bool hasValue= distance.TryGetValue(destination,out d);
            if (hasValue)
            {
                return int.MaxValue;
            }
            else
            {
                return d;
            }
        }
        public LinkedList<GameObject> getPath(GameObject target) {
            LinkedList<GameObject> path = new LinkedList<GameObject>();
            GameObject step = target;
            // check if a path exists
            if (predecessors[step] == null)
            {
                return null;
            }
            path.AddLast(step);
            while (predecessors[step] != null)
            {
                step = predecessors[step];
                path.AddLast(step);
            }
            // Put it into the correct order
            
            LinkedList<GameObject> newPath = new LinkedList<GameObject>();
            newPath = path;
            while(path.Count!=0)
                newPath.AddFirst(path.Last);
            return path;
        }
        public int pathCost(LinkedList<GameObject> path)
        {
            int pathDistance = 0;
            for (int i = 1; i < path.Count; i++)
            {
                pathDistance += getDistance(path.ElementAt(i - 1), path.ElementAt(i));
            }
            return pathDistance;
        }

    }
}
