using System.Collections.Generic;
using Core;

namespace Genetic_Algorithm
{
    public class GeneticAlgorithm
    {
        private List<Chromosome> population;

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
    }
}