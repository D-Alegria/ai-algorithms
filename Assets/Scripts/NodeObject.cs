using System;
using TMPro;
using UnityEngine;

public class NodeObject : MonoBehaviour
{
    public string value;
    public GameObject[] neighborNodes;
    public int[] costs;
    public Node Node { get; private set; }

    private void CreateNode()
    {
        (Node, int)[] neighbors = new (Node, int)[neighborNodes.Length];
        try
        {
            for (int i = 0; i < neighborNodes.Length; i++)
            {
                neighbors[i] = (neighborNodes[i].GetComponent<NodeObject>().Node, costs[i]);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        Node = new Node(value!, neighbors!);
    }

    void Awake()
    {
        CreateNode();
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text!.text = value!;
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