using System;
using System.Collections;
using System.Collections.Generic;
using Search_Algorithms;
using UnityEngine;

public class Searcher : MonoBehaviour
{
    public GameObject startingNodeObject;
    private ISearch _searchStrategy;

    void Start()
    {
        _searchStrategy = new DepthFirstSearch();
        var startingNode = startingNodeObject != null ? startingNodeObject.GetComponent<NodeObject>()?.Node : null;
        Debug.Log(_searchStrategy?.Search(startingNode, "E"));

        _searchStrategy = new IterativeDeepeningDfsSearch();
        var iterativeStartingNode =
            startingNodeObject != null ? startingNodeObject.GetComponent<NodeObject>()?.Node : null;
        Debug.Log(_searchStrategy?.Search(iterativeStartingNode, "E"));
    }
}

public interface ISearch
{
    public bool Search(Node currentNode, String targetValue);
}