using System;

public interface IObservable
{
    void NotifyObservers();
}

public interface IChangeNotifier : IObservable
{
    event Action ONStateChanged;
}