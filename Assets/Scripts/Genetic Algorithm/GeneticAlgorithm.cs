using System;
using Core;

namespace Genetic_Algorithm
{
    public partial class GeneticAlgorithm : IGeneticAlgorithmNotifier
    {
        private readonly int[,] _environment;
        private readonly int _chromosomeLength;
        private readonly int _populationSize;
        private readonly int _generationLimit;

        public GeneticAlgorithm(int[,] environment, int populationSize, int generationLimit)
        {
            _environment = environment;
            _chromosomeLength = environment.GetLength(0);
            _populationSize = populationSize;
            _population = new Chromosome[populationSize];
            _generationLimit = generationLimit;
        }

        public void RunEvolution()
        {
            GeneratePopulation();
            EvaluateFitness();
            for (var i = 1; i < _generationLimit; i++)
            {
                Evolve();
                EvaluateFitness();
                if(_fitnessValues[_fitnessValues.Length - 1].fitness >= 99f) break;
            }
        }

        public void NotifyObservers()
        {
            ONEvaluatedFitness?.Invoke(_fitnessValues);
        }

        public event Action<(Chromosome, float)[]> ONEvaluatedFitness;
    }
}