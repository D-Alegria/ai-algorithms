using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace Genetic_Algorithm
{
    public class GeneticAlgorithm
    {
        private List<Chromosome> population;
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();

        public List<Chromosome> GeneratePopulation(int length)
        {
            List<Chromosome> chromosomes = new List<Chromosome>();
            for (int i = 0; i < length; i++)
            {
                chromosomes.Add(new Chromosome().GenerateChromosome(10));
            }

            population = chromosomes;
            return chromosomes;
        }

        public (Chromosome, Chromosome) SinglePointCrossOver(Chromosome chromosomeA, Chromosome chromosomeB)
        {
            if (chromosomeA.Genes.Length != chromosomeB.Genes.Length)
            {
                return (chromosomeA, chromosomeB);
            }

            lock (SyncLock)
            {
                var point = Random.Next(chromosomeA.Genes.Length);
                var newGenesA = chromosomeA.Genes.Take(point).Concat(chromosomeB.Genes.Skip(point)).ToArray();
                var newGenesB = chromosomeB.Genes.Take(point).Concat(chromosomeA.Genes.Skip(point)).ToArray();

                chromosomeA.Genes = newGenesA;
                chromosomeB.Genes = newGenesB;

                return (chromosomeA, chromosomeB);
            }
        }
    }
}