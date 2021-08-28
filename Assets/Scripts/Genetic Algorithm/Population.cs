using System;
using Core;
using UnityEngine;

namespace Genetic_Algorithm
{
    public partial class GeneticAlgorithm
    {
        private Chromosome[] _population;
        private void GeneratePopulation()
        {
            for (int individual = 0; individual < _populationSize; individual++)
            {
                _population[individual] = CreateChromosome();
                // Debug.Log($"\n{individual} - {_population[individual]}");
            }
        }

        private Gene CreateGene(int locus, int allele, bool isColumnWise)
        {
            var pathDirection = Enum.GetValues(typeof(PathDirection));

            return new Gene(
                (locus, allele),
                (PathDirection) pathDirection.GetValue(RandomNumber.Generate(pathDirection.Length)),
                isColumnWise
            );
        }

        private Chromosome CreateChromosome()
        {
            var pathSwitch = new[] {RandomNumber.Generate(_chromosomeLength), RandomNumber.Generate(_chromosomeLength)};
            Array.Sort(pathSwitch);
            var genes = new Gene[_chromosomeLength];
            var isColumnWise = RandomNumber.Generate(2) > 0;

            for (int locus = 0; locus < _chromosomeLength; locus++)
            {
                int allele = locus;
                if (locus != 0 && locus != _chromosomeLength - 1) allele = RandomNumber.Generate(_chromosomeLength);
                genes[locus] = CreateGene(locus, allele, isColumnWise);
            }

            return new Chromosome(genes, pathSwitch);
        }
    }
}