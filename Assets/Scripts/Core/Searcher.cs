using Search_Algorithms;
using TMPro;
using UnityEngine;

namespace Core
{
    public class Searcher : MonoBehaviour
    {
        public GameObject startingNodeObject;
        public TMP_Dropdown searchTitle;
        public string searchTarget;
        private ISearch _searchStrategy;
        private bool _isRunning;

        void OnEnable()
        {
            searchTitle.onValueChanged.AddListener(delegate { SetAlgorithm(); });
        }

        private void SetAlgorithm()
        {
            _searchStrategy = searchTitle.value switch
            {
                0 => new DepthFirstSearch(),
                1 => new BreadthFirstSearch(),
                2 => new IterativeDeepeningSearch(2),
                3 => new DepthLimitedSearch(2),
                4 => new UniformCostSearch(),
                _ => _searchStrategy
            };
        }

        public void Search()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                Session.Instance.StartSession();
                Ticker.Instance.TimeInSeconds = 0;
                Node startingNode = startingNodeObject != null ? startingNodeObject.GetComponent<NodeObject>()?.Node : null;
                SetAlgorithm();
                _searchStrategy?.Search(startingNode, searchTarget);
            }
            else
            {
                Session.Instance.EndSession();
                _isRunning = false;
            }
        }
    }
}