using System.Linq;
using Core;
using UnityEngine;

namespace Genetic_Algorithm
{
    public partial class GeneticAlgorithm
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
            GenerateFitnessParameters();
        }

        // Select
        // Crossover
        public (Chromosome, Chromosome) SinglePointCrossOver(Chromosome chromosomeA, Chromosome chromosomeB)
        {
            if (chromosomeA.Genes.Length != chromosomeB.Genes.Length)
            {
                return (chromosomeA, chromosomeB);
            }

            var point = RandomNumber.Generate(chromosomeA.Genes.Length);
            var newGenesA = chromosomeA.Genes.Take(point).Concat(chromosomeB.Genes.Skip(point)).ToArray();
            var newGenesB = chromosomeB.Genes.Take(point).Concat(chromosomeA.Genes.Skip(point)).ToArray();

            return (
                new Chromosome(newGenesA, chromosomeA.IsColumnWise, chromosomeA.PathSwitch),
                new Chromosome(newGenesB, chromosomeB.IsColumnWise, chromosomeB.PathSwitch)
            );
        }
        // Mutate
    }
}