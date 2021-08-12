using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Search_Algorithms
{
    public class BiDirectionalSearch : ISearch
    {
        private bool _found; // used in Traverse
        private bool _tailFound; // used in FindTail
        private List<Node> _fromHead = new List<Node>();
        private List<Node> _fromTail = new List<Node>();

        public BiDirectionalSearch(Node tail)
        {
            _fromTail.Add(tail); // Adds the current/start node to the _fromHead list
        }

        private bool FindTail(Node currentNode, String targetValue)
        {
            // Searches until it finds the node whose value is equal to the targetValue. Adds that node to the _fromTail list so it can be used for the reverse traversal.
            if (currentNode.Value == targetValue)
            {
                _fromTail.Add(currentNode);
                return _tailFound = true;
            }

            for (int i = 0; i < currentNode.Neighbors.Length; i++)
            {
                if (currentNode.Neighbors[i].node.Visited) continue;
                FindTail(currentNode.Neighbors[i].node, targetValue);
                if (_tailFound) return _tailFound;
            }

            return false;
        }

        private bool Traverse(int currentIndex)
        {
            Debug.Log($"Current index {currentIndex}");
            // _fromHead.ForEach(node => Debug.Log($"_fromHead {node.Value}"));
            // _fromTail.ForEach(node => Debug.Log($"_fromTail {node.Value}"));
            // Get current node starting traversal from head, and current node starting traversal from tail
            Node headNode = _fromHead[currentIndex];
            Node tailNode = _fromTail[currentIndex];
            int loopLength = Math.Max(headNode.Neighbors.Length, tailNode.Neighbors.Length);
            int j = 1;
            var visitedHead = headNode.Value;
            var visitedTail = tailNode.Value;

            if (_fromHead.Contains(tailNode) || _fromTail.Contains(headNode))
            {
                return _found = true;
            }

            for (int i = 0; i < loopLength; i++)
            {
                // Add next headNode neighbour to _fromHead list, and next tailNode neighbour to _fromTail list, so that both can be retrieved during the next recursive Traverse() call
                if (i < headNode.Neighbors.Length)
                {
                    // Loop from this index to the end of the headNode neighbours list to check if there is any ideal candidate as the next neighbour
                    for (j = i; j < headNode.Neighbors.Length; j++)
                    {
                        if (!headNode.Neighbors[j].node.Visited) break;
                    }

                    if (j == headNode.Neighbors.Length) continue;


                    _fromHead.Add(headNode.Neighbors[j].node);
                }

                if (i < tailNode.Neighbors.Length)
                {
                    for (j = i; j < tailNode.Neighbors.Length; j++)
                    {
                        if (!tailNode.Neighbors[j].node.Visited) break;
                    }

                    if (j == tailNode.Neighbors.Length) continue;

                    String value = tailNode.Neighbors[j].node.Value;

                    _fromTail.Add(tailNode.Neighbors[j].node);
                }

                Traverse(currentIndex + 1);
                if (_found) return _found;
            }

            return false;
        }

        public bool Search(Node currentNode, String targetValue)
        {
            _fromHead.Add(currentNode); // Adds thet current/start node to the _fromHead list
            // FindTail(currentNode, targetValue); // Adds the last/target node to the _fromTail list
            Traverse(0); // Recursively traverse neighbours adding the traversal paths from the start and target nodes to two lists called _fromHead and _fromTail
            _fromTail.Reverse();

            foreach (var node in _fromHead.Concat(_fromTail))
            {
                Debug.Log($"Node {node.Value}");
            }

            return false;
        }
    }
}