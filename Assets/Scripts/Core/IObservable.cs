using System;
using Core;

public interface IObservable
{
    void NotifyObservers();
}

public interface IChangeNotifier : IObservable
{
    event Action ONStateChanged;
}

public interface IGeneticAlgorithmNotifier : IObservable
{
    event Action<(Chromosome, float)[]> ONEvaluatedFitness;
}