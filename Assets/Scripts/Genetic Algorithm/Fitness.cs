using System;
using System.Linq;
using Core;
using UnityEngine;

namespace Genetic_Algorithm
{
    public partial class GeneticAlgorithm
    {
        //fitness parameters
        private int[] _pathLengths;
        private int[] _amountsOfCollisions;
        private int[] _amountsOfTurns;
        private int _longestPathLength;
        private int _lengthFactor;
        private int _highestCollisions;
        private int _collisionFactor;
        private int _highestTurns;

        private int _turnFactor;

        // fitness values
        private (Chromosome chromosome, float fitness)[] _fitnessValues;

        private void GenerateFitnessParameters() // Generates values for the parameters used in evaluating fitness
        {
            _pathLengths = new int[_populationSize];
            _amountsOfTurns = new int[_populationSize];
            _amountsOfCollisions = new int[_populationSize];

            for (int individual = 0; individual < _population.Length; individual++)
            {
                Chromosome chromosome = _population[individual];
                _pathLengths[individual] = GetPathLength(chromosome.Genes, chromosome.PathSwitch);
                _amountsOfCollisions[individual] =
                    GetPathCollisions(chromosome.Genes, chromosome.PathSwitch);
                _amountsOfTurns[individual] = GetPathTurns(chromosome.Genes, chromosome.PathSwitch);
            }

            _longestPathLength = _pathLengths.Max();
            _lengthFactor = _longestPathLength - _pathLengths.Min();
            _highestCollisions = _amountsOfCollisions.Max();
            _collisionFactor = _highestCollisions - _amountsOfCollisions.Min();
            _highestTurns = _amountsOfTurns.Max();
            _turnFactor = _highestTurns - _amountsOfTurns.Min();
        }

        private int GetPathLength(Gene[] genes, int[] pathSwitch)
        {
            int length = 0;

            for (int i = 0; i < genes.Length - 1; i++)
            {
                var geneA = genes[i];
                var geneB = genes[i + 1];

                //Calculating path length depending on the positions of geneA and geneB taking the switching points into consideration
                if (pathSwitch[0] != pathSwitch[1] && Array.Exists(pathSwitch, el => el == i))
                {
                    length += Math.Abs(geneB.PathLocation.allelle - geneA.PathLocation.locus) +
                              Math.Abs(geneB.PathLocation.locus - geneA.PathLocation.allelle);
                }
                else
                {
                    length += (geneB.PathLocation.locus - geneA.PathLocation.locus) +
                              Math.Abs(geneB.PathLocation.allelle - geneA.PathLocation.allelle);
                }
            }

            return length;
        }

        private int GetPathCollisions(Gene[] genes, int[] pathSwitch)
        {
            int collisions = 0;

            for (int i = 0; i < genes.Length - 1; i++)
            {
                // getting current gene and next gene for comparison
                var geneA = genes[i];
                var geneB = genes[i + 1];

                // checking whether geneA is column or row wise and setting environment location pointer accordingly
                (int row, int col) locationA = (!geneA.IsColumnWise ^ (i > pathSwitch[0] && i <= pathSwitch[1]))
                    ? (geneA.PathLocation.locus, geneA.PathLocation.allelle)
                    : (geneA.PathLocation.allelle, geneA.PathLocation.locus);

                // checking whether geneB is column or row wise and setting environment location pointer accordingly
                (int row, int col) locationB = (!geneB.IsColumnWise ^ (i + 1 > pathSwitch[0] && i + 1 <= pathSwitch[1]))
                    ? (geneB.PathLocation.locus, geneB.PathLocation.allelle)
                    : (geneB.PathLocation.allelle, geneB.PathLocation.locus);

                // used in the following loops to control whether they move in the +ve or -ve directions
                int colDirection = locationA.col <= locationB.col ? 1 : -1;
                int rowDirection = locationA.row <= locationB.row ? 1 : -1;

                if (geneA.PathDirection == PathDirection.Horizontal)
                {
                    // First moving horizontally from locationA's column to locationB's column and adding all detected collisions
                    int col = locationA.col;
                    while (col != locationB.col)
                    {
                        col += colDirection;
                        if (_environment[locationA.row, col] == 0) collisions++;
                    }

                    // then continuing vertically to locationB's row and adding all detected collisions
                    int row = locationA.row;
                    while (row != locationB.row)
                    {
                        row += rowDirection;
                        if (_environment[row, locationB.col] == 0) collisions++;
                    }
                }
                else
                {
                    // First moving vertically from locationA's row to locationB's row and adding all detected collisions
                    int row = locationA.row;
                    while (row != locationB.row)
                    {
                        row += rowDirection;
                        if (_environment[row, locationA.col] == 0) collisions++;
                    }

                    // then continuing horizontally to locationB's column and adding all detected collisions
                    int col = locationA.col;
                    while (col != locationB.col)
                    {
                        col += colDirection;
                        if (_environment[locationB.row, col] == 0) collisions++;
                    }
                }
            }

            return collisions;
        }

        private int GetPathTurns(Gene[] genes, int[] pathSwitch)
        {
            int numberOfTurns = 0;

            for (int i = 0; i < genes.Length - 1; i++)
            {
                var geneA = genes[i];
                var geneB = genes[i + 1];

                //Calculating number of turns required in moving from geneA and geneB taking the switching points into consideration
                if (pathSwitch[0] != pathSwitch[1] && Array.Exists(pathSwitch, el => el == i))
                {
                    if (geneB.PathLocation.locus != geneA.PathLocation.allelle &&
                        geneB.PathLocation.allelle != geneA.PathLocation.locus)
                    {
                        numberOfTurns += 1;
                    }
                }
                else
                {
                    if (geneB.PathLocation.locus != geneA.PathLocation.locus &&
                        geneB.PathLocation.allelle != geneA.PathLocation.allelle)
                    {
                        numberOfTurns += 1;
                    }
                }
            }

            return numberOfTurns;
        }

        private void EvaluateFitness()
        {
            GenerateFitnessParameters();

            _fitnessValues = new (Chromosome, float)[_populationSize];

            for (int i = 0; i < _fitnessValues.Length; i++)
            {
                var fLength = (_longestPathLength - _pathLengths[i]) / (float) _lengthFactor;
                var fCollisions = (_highestCollisions - _amountsOfCollisions[i]) / (float) _collisionFactor;
                var fNumberOfTurns = (_highestTurns - _amountsOfTurns[i]) / (float) _turnFactor;

                if (double.IsNaN(fLength)) fLength = 1;
                if (double.IsNaN(fCollisions)) fCollisions = 1;
                if (double.IsNaN(fNumberOfTurns)) fNumberOfTurns = 1;

                // var fPath = fCollisions * (fLength + 2 * fNumberOfTurns) * 100 / (3);
                var fPath = fCollisions * (4 * fLength + 2 * fNumberOfTurns) * 100 / 6;

                if (_amountsOfCollisions[i] > 0)
                {
                    fPath = 0.1f * fPath / (float) Math.Pow(_amountsOfCollisions[i], 2);
                }

                _fitnessValues[i] = (_population[i], fPath);
            }

            Array.Sort(_fitnessValues, (a, b) => a.fitness.CompareTo(b.fitness));

            NotifyObservers();
            Debug.Log($"{string.Join("\n \n", _fitnessValues)}");
        }
    }
}