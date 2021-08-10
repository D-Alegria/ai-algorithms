using System;
using Core;
using Search_Algorithms;
using TMPro;
using UnityEngine;

public class Searcher : MonoBehaviour
{
    public GameObject startingNodeObject;
    public TMP_Text searchTitle;
    public string searchTarget;
    private ISearch _searchStrategy;

    public SearchAlgorithms searchAlgorithm;

    void Start()
    {
        switch (searchAlgorithm)
        {
            case SearchAlgorithms.DepthFirstSearch:
                searchTitle.text = "Depth First Search";
                _searchStrategy = new DepthFirstSearch();
                break;
            case SearchAlgorithms.BreadthFirstSearch:
                searchTitle.text = "Breadth First Search";
                _searchStrategy = new BreadthFirstSearch();
                break;
            case SearchAlgorithms.IterativeDeepeningSearch:
                searchTitle.text = "Iterative Deepening Search";
                _searchStrategy = new IterativeDeepeningSearch(2);
                break;
            case SearchAlgorithms.DepthLimitedSearch:
                searchTitle.text = "Depth Limited Search";
                _searchStrategy = new DepthLimitedSearch(2);
                break;
            case SearchAlgorithms.UniformCostSearch:
                searchTitle.text = "Uniform Cost Search";
                _searchStrategy = new UniformCostSearch();
                break;
        }

        Node startingNode = startingNodeObject != null ? startingNodeObject.GetComponent<NodeObject>()?.Node : null;
        Debug.Log(_searchStrategy?.Search(startingNode, searchTarget));
    }
}

public enum SearchAlgorithms
{
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