using System;

namespace Search_Algorithms
{
    public class DepthFirstSearch : ISearch
    {
        private bool _found;

        public bool Search(Node currentNode, String targetValue)
        {
            if (currentNode.Value == targetValue) return _found = true;

            for (int i = 0; i < currentNode.Neighbors.Length; i++)
            {
                if(currentNode.Neighbors[i].node.Visited) continue;
                Search(currentNode.Neighbors[i].node, targetValue);
                if (_found) return _found;
            }

            return false;
        }
    }
}