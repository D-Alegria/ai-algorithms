using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search : MonoBehaviour
{
    public GameObject startingNodeObject;
    public ISearch searchStrategy;

    void Start()
    {
        Node startingNode = startingNodeObject != null ? startingNodeObject.GetComponent<NodeObject>()?.Node : null;
        Node next = startingNode?.Neighbors?[0].node;
        searchStrategy?.Search(startingNode);
    }

    void Update()
    {
    }
}

public interface ISearch
{
    public void Search(Node startingNode);
}