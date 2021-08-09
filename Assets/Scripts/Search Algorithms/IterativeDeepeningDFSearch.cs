using System;
using System.Collections.Generic;
using System.Linq;

namespace Search_Algorithms
{
    public class IterativeDeepeningDFSearch : ISearch
    {
        private bool _found;
        private int _depthLimit;
        private Queue<Node> _neighbors = new Queue<Node>();

        public IterativeDeepeningDFSearch(int depthLimit)
        {
            _depthLimit = depthLimit;
        }

        public bool Search(Node currentNode, String targetValue)
        {
            if (DepthLimitedSearch(currentNode, targetValue, 0)) return true;
            while (_neighbors.Count > 0)
            {
                if (BreadthFirstSearch(_neighbors.Dequeue(), targetValue)) return true;
            }
            return false;
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

        private bool BreadthFirstSearch(Node currentNode, String targetValue)
        {
            if (currentNode.Value == targetValue) return _found = true;

            for (int i = 0; i < currentNode.Neighbors.Length; i++)
            {
                if (currentNode.Neighbors[i].node.Visited) continue;
                _neighbors.Enqueue(currentNode);
            }

            return false;
        }
    }
}