using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searcher : MonoBehaviour
{
    public GameObject startingNodeObject;
    private ISearch _searchStrategy;

    void Start()
    {
        _searchStrategy = new DepthFirstSearch();
        Node startingNode = startingNodeObject != null ? startingNodeObject.GetComponent<NodeObject>()?.Node : null;
        Debug.Log(_searchStrategy?.Search(startingNode, "E"));
    }
}

public interface ISearch
{
    public bool Search(Node currentNode, String targetValue);
}