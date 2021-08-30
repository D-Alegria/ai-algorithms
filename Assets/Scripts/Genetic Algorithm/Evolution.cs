using System;
using System.Linq;
using Core;
using UnityEngine;

namespace Genetic_Algorithm
{
    public partial class GeneticAlgorithm
    {
        private void Evolve()
        {
            Chromosome[] newGeneration = new Chromosome [_populationSize];
            newGeneration[0] = _fitnessValues[_fitnessValues.Length - 1].chromosome;

            for (int individual = 1; individual < _populationSize; individual += 2)
            {
                var (firstChild, secondChild) = SinglePointCrossOver(SelectParent(), SelectParent());
                newGeneration[individual] = Mutate(firstChild);
                if (individual + 1 < _populationSize)
                {
                    newGeneration[individual + 1] = Mutate(secondChild);
                }
            }

            _population = newGeneration;
        }

        // Select
        private Chromosome SelectParent()
        {
            int totalWeights = _fitnessValues.Length * (_fitnessValues.Length + 1) / 2;
            int selection = RandomNumber.Generate(1, totalWeights + 1);
            int selectionIndex = 0;
            int lowerBound = 0;
            
            int i = 1;
            while (selection > lowerBound)
            {
                selectionIndex = i - 1;
                lowerBound += i;
                i++;
            }

            return _fitnessValues[selectionIndex].chromosome;
        }

        // Crossover
        private (Chromosome a, Chromosome b) SinglePointCrossOver(Chromosome chromosomeA, Chromosome chromosomeB)
        {
            var point = RandomNumber.Generate(chromosomeA.Genes.Length);
            var newGenesA = chromosomeA.Genes.Take(point).Concat(chromosomeB.Genes.Skip(point)).ToArray();
            var newGenesB = chromosomeB.Genes.Take(point).Concat(chromosomeA.Genes.Skip(point)).ToArray();

            return (
                new Chromosome(newGenesA, chromosomeA.PathSwitch),
                new Chromosome(newGenesB, chromosomeB.PathSwitch)
            );
        }

        // Mutate
        private Chromosome Mutate(Chromosome chromosome)
        {
            var mutationProbability = 0.45f;
            
            for (var locus = 0; locus < chromosome.Genes.Length; locus++)
            {
                Gene mutatedGene = chromosome.Genes[locus];
                
                if (RandomNumber.Generate(101) / 100f < mutationProbability)
                {
                    var direction = RandomNumber.Generate(2);
                    var pathDirection = Enum.GetValues(typeof(PathDirection));
                    
                    mutatedGene.IsColumnWise = direction > 0;
                    mutatedGene.PathDirection =
                        (PathDirection) pathDirection.GetValue(RandomNumber.Generate(pathDirection.Length));
                    
                    int allele = locus;
                    if (locus != 0 && locus != _chromosomeLength - 1) allele = RandomNumber.Generate(_chromosomeLength);
                    mutatedGene.PathLocation = (locus, allele);
                }

                chromosome.Genes[locus] = mutatedGene;
            }

            if (RandomNumber.Generate(11) / 10f > mutationProbability)
            {
                var pathSwitch = new[] {RandomNumber.Generate(_chromosomeLength), RandomNumber.Generate(_chromosomeLength)};
                Array.Sort(pathSwitch);

                chromosome.PathSwitch = pathSwitch;
            }

            return chromosome;
        }
    }
}