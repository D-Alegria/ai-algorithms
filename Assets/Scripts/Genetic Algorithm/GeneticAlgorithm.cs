using System;
using System.Collections;
using Core;

namespace Genetic_Algorithm
{
    public partial class GeneticAlgorithm : IGeneticAlgorithmNotifier
    {
        private readonly int[,] _environment;
        private readonly int _chromosomeLength;
        private readonly int _populationSize;
        private readonly int _generationLimit;
        private int _currentGeneration;
        private readonly float _fitnessTolerance;

        public GeneticAlgorithm(int[,] environment, int populationSize, int generationLimit, float fitnessTolerance)
        {
            _environment = environment;
            _chromosomeLength = environment.GetLength(0);
            _populationSize = populationSize;
            _population = new Chromosome[populationSize];
            _generationLimit = generationLimit;
            _fitnessTolerance = fitnessTolerance;
        }

        public void RunEvolution()
        {
            GeneratePopulation();
            EvaluateFitness();
            for (_currentGeneration = 1; _currentGeneration < _generationLimit; _currentGeneration++)
            {
                Evolve();
                EvaluateFitness();
                if (_fitnessValues[_fitnessValues.Length - 1].fitness >= _fitnessTolerance) break;
            }
        }

        public IEnumerable NextGeneration()
        {
            for (_currentGeneration = 0; _currentGeneration < _generationLimit; _currentGeneration++)
            {
                if (_currentGeneration == 0)
                {
                    GeneratePopulation();
                }
                else
                {
                    if (_fitnessValues[_fitnessValues.Length - 1].fitness >= _fitnessTolerance) yield break;
                    Evolve();
                }

                EvaluateFitness();
                yield return null;
            }
        }

        public void NotifyObservers()
        {
            ONEvaluatedFitness?.Invoke(_fitnessValues);
        }

        public event Action<(Chromosome, float)[]> ONEvaluatedFitness;
    }
}