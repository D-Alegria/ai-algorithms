using Core;

namespace Search_Algorithms
{
    public class UniformCostSearch : ISearch
    {
        private readonly PriorityQueue<PriorityNode> _activeNodes = new PriorityQueue<PriorityNode>();

        public bool Search(Node currentNode, string targetValue)
        {
            _activeNodes.Enqueue(new PriorityNode(currentNode, 0));

            while (_activeNodes.Count() != 0)
            {
                var topPriorityNode = _activeNodes.Dequeue();
                var currentHead = topPriorityNode.Node;
                var currentMinimumCost = topPriorityNode.Cost;
                if (currentHead.Value == targetValue) return true;

                foreach (var node in currentHead.Neighbors)
                {
                    if (node.node.Visited) continue;
                    _activeNodes.Enqueue(new PriorityNode(node.node, node.cost + currentMinimumCost));
                }
            }

            return false;
        }
    }
}