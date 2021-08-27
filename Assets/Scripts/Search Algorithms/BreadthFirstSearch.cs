using System.Collections.Generic;
using Core;

namespace Search_Algorithms
{
    public class BreadthFirstSearch : ISearch
    {
        private Queue<Node> _neighbors;

        public BreadthFirstSearch()
        {
            _neighbors = new Queue<Node>();
        }

        public BreadthFirstSearch(Queue<Node> neighbors)
        {
            _neighbors = neighbors;
        }

        public bool Search(Node currentNode, string targetValue)
        {
            if (_neighbors.Count < 1) _neighbors.Enqueue(currentNode);
            while (_neighbors.Count > 0)
            {
                currentNode = _neighbors.Dequeue();
                if (currentNode.Value == targetValue) return true;

                for (int i = 0; i < currentNode.Neighbors.Length; i++)
                {
                    if (currentNode.Neighbors[i].node.Visited) continue;
                    _neighbors.Enqueue(currentNode.Neighbors[i].node);
                }
            }

            return false;
        }
    }
}