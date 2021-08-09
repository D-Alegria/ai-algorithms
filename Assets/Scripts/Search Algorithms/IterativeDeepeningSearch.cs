using System;
using System.Collections.Generic;

namespace Search_Algorithms
{
    public class IterativeDeepeningSearch : ISearch
    {
        private bool _found;
        private int _depthLimit;
        private Queue<Node> _neighbors = new Queue<Node>();

        public IterativeDeepeningSearch(int depthLimit)
        {
            _depthLimit = depthLimit;
        }

        public bool Search(Node currentNode, String targetValue)
        {
            if (DepthLimitedSearch(currentNode, targetValue, 0)) return true;
            return new BreadthFirstSearch(_neighbors).Search(currentNode, targetValue);
        }

        private bool DepthLimitedSearch(Node currentNode, String targetValue, int depth)
        {
            if (currentNode.Value == targetValue) return _found = true;
            if (depth + 1 > _depthLimit)
            {
                for (int i = 0; i < currentNode.Neighbors.Length; i++)
                {
                    _neighbors.Enqueue(currentNode.Neighbors[i].node);
                }

                return false;
            }

            for (int i = 0; i < currentNode.Neighbors.Length; i++)
            {
                if (currentNode.Neighbors[i].node.Visited) continue;
                DepthLimitedSearch(currentNode.Neighbors[i].node, targetValue, depth + 1);
                if (_found) return _found;
            }

            return false;
        }
    }
}