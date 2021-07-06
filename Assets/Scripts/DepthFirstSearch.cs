using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearch : ISearch
{
    public void Search(Node startingNode)
    {
        throw new System.NotImplementedException();
    }
    
    public int MaxAreaOfIsland(int[][] grid)
    {
        var visited = new HashSet<KeyValuePair<int, int>>();
        var largestArea = 0;

        void DepthFirstSearch(KeyValuePair<int, int> coordinate, RefInt islandArea)
        {
            islandArea ??= new RefInt(0);

            if (!visited.Add(coordinate)) return;
            if (grid[coordinate.Value][coordinate.Key] == 1)
            {
                islandArea.Increment();
                // checks above
                if (coordinate.Value - 1 >= 0)
                    DepthFirstSearch(new KeyValuePair<int, int>(coordinate.Key, coordinate.Value - 1), islandArea);
                // checks below
                if (coordinate.Value + 1 < grid.Length)
                    DepthFirstSearch(new KeyValuePair<int, int>(coordinate.Key, coordinate.Value + 1), islandArea);
                // checks the left
                if (coordinate.Key - 1 >= 0)
                    DepthFirstSearch(new KeyValuePair<int, int>(coordinate.Key - 1, coordinate.Value), islandArea);
                // checks the right
                if (coordinate.Key + 1 < grid[0].Length)
                    DepthFirstSearch(new KeyValuePair<int, int>(coordinate.Key + 1, coordinate.Value), islandArea);
            }

            if (islandArea.Value > largestArea) largestArea = islandArea.Value;
        }

        for (var y = 0; y < grid.Length - 1; y++)
        {
            for (var x = 0; x < grid[0].Length; x++)
            {
                DepthFirstSearch(new KeyValuePair<int, int>(x, y), null);
            }
        }

        return largestArea;
    }
}

internal class RefInt
{
    public int Value { get; private set; }

    public RefInt(int value)
    {
        Value = value;
    }

    public void Increment()
    {
        Value++;
    }
}