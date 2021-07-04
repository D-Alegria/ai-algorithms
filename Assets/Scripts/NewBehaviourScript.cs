using UnityEditor;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class NewBehaviourScript : ScriptableObject
    {
        public GameObject[] nodes;
        // Start is called before the first frame update
        void Start()
        {
            nodes = GameObject.FindGameObjectsWithTag("Node");
        }

        void Awake()
        {
            nodes = GameObject.FindGameObjectsWithTag("Node");
        }

        void Enabled()
        {
            nodes = GameObject.FindGameObjectsWithTag("Node");
        }
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}