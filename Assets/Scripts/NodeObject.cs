using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NodeObject : MonoBehaviour
{
    public string value;
    public GameObject[] neighborNodes;
    public int[] costs;
    public Node Node;
    private Renderer _nodeRenderer;
    private static readonly int Albedo = Shader.PropertyToID("albedo");
    private Ticker _ticker = Ticker.Instance;

    void Awake()
    {
        (Node, int)[] neighbors = new (Node, int)[neighborNodes.Length];
        Node = new Node(value!, neighbors!);

        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text!.text = value!;

        _nodeRenderer = gameObject.GetComponent<Renderer>();
    }

    private void Start()
    {
        PopulateNode();
    }

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
        StartCoroutine(WaitThenSetColor(1));
        _ticker.Tick(1);
    }

    private IEnumerator WaitThenSetColor(int seconds)
    {
        yield return new WaitForSeconds(_ticker.TimeInSeconds);
        _nodeRenderer.material.color = Color.blue;
    }
}