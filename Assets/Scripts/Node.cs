using System;

public class Node : IChangeNotifier
{
    private string _value;

    public string Value
    {
        get
        {
            Visit();
            return _value;
        }
        set => _value = value;
    }

    private (Node, int)[] _neighbors;

    public (Node node, int cost)[] Neighbors
    {
        get
        {
            Visit();
            return _neighbors.Length < 1 ? null : _neighbors;
        }
        set => _neighbors = value;
    }

    public bool Visited { get; private set; }

    public Node(string value, (Node, int)[] neighbors)
    {
        _value = value;
        _neighbors = neighbors;
        Visited = false;
    }

    public event Action ONStateChanged;

    private void Visit()
    {
        Visited = true;
        NotifyObservers();
    }

    public void NotifyObservers()
    {
        ONStateChanged?.Invoke();
    }
}