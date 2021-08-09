using System;

namespace Search_Algorithms
{
    public class DepthLimitedSearch : ISearch
    {
        private bool _found;
        private readonly int _depthLimit;

        public DepthLimitedSearch(int depthLimit)
        {
            _depthLimit = depthLimit;
        }

        public bool Search(Node currentNode, String targetValue)
        {
            return Search(currentNode, targetValue, 0);
        }

        private bool Search(Node currentNode, String targetValue, int depth)
        {
            if (currentNode.Value == targetValue) return _found = true;
            if (depth + 1 > _depthLimit) return false;

            for (int i = 0; i < currentNode.Neighbors.Length; i++)
            {
                if (currentNode.Neighbors[i].node.Visited) continue;
                Search(currentNode.Neighbors[i].node, targetValue, depth + 1);
                if (_found) return _found;
            }

            return false;
        }
    }
}