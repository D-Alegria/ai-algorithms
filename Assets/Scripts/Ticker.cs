using UnityEngine;

public sealed class Ticker
{
    private Ticker()
    {
    }

    private static Ticker _instance;

    public static Ticker Instance
    {
        get { return _instance ??= new Ticker(); }
    }

    public int TimeInSeconds;

    public void Tick(int seconds)
    {
        TimeInSeconds += seconds;
    }
}