using Core;
using Search_Algorithms;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Searcher : MonoBehaviour
{
    public GameObject startingNodeObject;
    public GameObject endingNodeObject;
    public TMP_Dropdown searchTitle;
    public string searchTarget;
    private ISearch _searchStrategy;
    private bool _isRunning;

    public SearchAlgorithms searchAlgorithm;

    void Start()
    {
        searchTitle.onValueChanged.AddListener(delegate { SetAlgorithm(); });
        SetAlgorithm();
        Debug.Log(searchTitle.value);
    }

    private void SetAlgorithm()
    {
        switch (searchTitle.value)
        {
            case 0:
                _searchStrategy = new DepthFirstSearch();
                break;
            case 1:
                _searchStrategy = new BreadthFirstSearch();
                break;
            case 2:
                _searchStrategy = new IterativeDeepeningSearch(2);
                break;
            case 3:
                _searchStrategy = new DepthLimitedSearch(2);
                break;
            case 4:
                _searchStrategy = new UniformCostSearch();
                break;
            case 5:
                Node endingNode = endingNodeObject != null ? endingNodeObject.GetComponent<NodeObject>()?.Node : null;
                _searchStrategy = new BiDirectionalSearch(endingNode);
                break;
        }
    }

    public void Search()
    {
        if (!_isRunning)
        {
            Session.Instance.StartSession();
            _isRunning = true;
            Node startingNode = startingNodeObject != null ? startingNodeObject.GetComponent<NodeObject>()?.Node : null;
            _searchStrategy?.Search(startingNode, searchTarget);
        }
        else
        {
            Session.Instance.EndSession();
            _isRunning = false;
        }
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