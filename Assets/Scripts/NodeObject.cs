using System;
using TMPro;
using UnityEngine;

public class NodeObject : MonoBehaviour
{
    public string value;
    public GameObject[] neighborNodes;
    public int[] costs;
    public Node Node;

    private void PopulateNode()
    {
        try
        {
            for (int i = 0; i < neighborNodes.Length; i++)
            {
                Node.Neighbors[i] = (neighborNodes[i].GetComponent<NodeObject>().Node, costs[i]);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    void Awake()
    {
        (Node, int)[] neighbors = new (Node, int)[neighborNodes.Length];
        Node = new Node(value!, neighbors!);
        
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text!.text = value!;
    }

    private void Start()
    {
        PopulateNode();
    }

    private void OnEnable()
    {
        Node!.ONStateChanged += SetColor;
    }

    private void OnDisable()
    {
        Node!.ONStateChanged -= SetColor;
    }

    private void SetColor()
    {
        Debug.Log(Node!.Visited);
    }
}