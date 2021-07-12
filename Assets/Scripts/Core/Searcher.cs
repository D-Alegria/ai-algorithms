using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Search_Algorithms;
using UnityEngine;

public class Searcher : MonoBehaviour
{
    public GameObject startingNodeObject;
    private ISearch _searchStrategy;

    public SearchAlgorithms searchAlgorithm;

    void Start()
    {
        switch (searchAlgorithm)
        {
            case SearchAlgorithms.DepthFirstSearch:
                _searchStrategy = new DepthFirstSearch();
                break;
        }
        Node startingNode = startingNodeObject != null ? startingNodeObject.GetComponent<NodeObject>()?.Node : null;
        Debug.Log(_searchStrategy?.Search(startingNode, "6"));
    }
}

public enum SearchAlgorithms{
    DepthFirstSearch,
    BreadthFirstSearch,
    UniformCostSearch,
    IterativeDeepeningSearch,
    DepthLimitedSearch,
    BiDirectionalSearch,
}

public interface ISearch
{
    public bool Search(Node currentNode, String targetValue);
}