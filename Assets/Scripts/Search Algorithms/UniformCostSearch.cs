using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Search_Algorithms
{
    public class UniformCostSearch : ISearch
    {
        Dictionary<string, Node> activeNodes = new Dictionary<String, Node>();
        Dictionary<string, int> store = new Dictionary<string, int>();
        private static Random random = new Random();

        public bool Search(Node currentNode, string targetValue)
        {
            return Expand(currentNode, targetValue, 0);
        }

        private bool Expand(Node currentNode, string targetValue, int minCost)
        {
            Debug.Log(minCost);
            if (currentNode.Value == targetValue) return true;
            Debug.Log($"currentNode {currentNode.Value}");
            foreach (var node in currentNode.Neighbors)
            {
                if (node.node.Visited) continue;
                string key = "";
                while (!store.ContainsKey(key) && key.Length < 1)
                {
                    key = RandomString();
                }

                store.Add(key, node.cost);
                activeNodes.Add(key, node.node);
            }

            if (activeNodes.Count == 0) return false;
            int minimumCost = store.Values.Min();
            string costKey = store.FirstOrDefault(pair => pair.Value == minimumCost).Key;
            Node nextNode = activeNodes[costKey];
            activeNodes.Remove(costKey);
            store.Remove(costKey);
            minCost += minimumCost;
            return Expand(nextNode, targetValue, minCost);
        }


        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 2)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}