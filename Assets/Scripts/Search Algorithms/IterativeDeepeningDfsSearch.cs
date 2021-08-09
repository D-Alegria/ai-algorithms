using System;
using System.Linq;

namespace Search_Algorithms
{
    public class IterativeDeepeningDfsSearch : ISearch
    {
        private int _bound = 0;
        private int depth = 0;

        public bool Search(Node currentNode, String targetValue)
        {
            if (currentNode.Value == targetValue) return true;
            if (currentNode.Neighbors.Length == 0) return false;
            depth = GetDepth(currentNode, 0,0);
            return currentNode.Neighbors.Any(nextNode => BoundedSearch(nextNode.node, targetValue, 1));
        }

        private bool BoundedSearch(Node currentNode, String targetValue, int currentDepth)
        {
            if (currentNode.Value == targetValue) return true;
            if (_bound < currentDepth || currentNode.Neighbors.Length == 0) return false;
            currentDepth++;
            return currentNode.Neighbors.Any(nextNode => BoundedSearch(nextNode.node, targetValue, currentDepth));
        }

        private static int GetDepth(Node currentNode, int currentDepth, int maxDepth)
        {
            currentDepth++;
            foreach (var neighbor in currentNode.Neighbors)
            {
                if (neighbor.node.Visited) continue;
                GetDepth(neighbor.node, currentDepth, maxDepth);
            }

            return Math.Max(currentDepth, maxDepth);
        }
    }
}