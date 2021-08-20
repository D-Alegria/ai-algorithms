using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Chromosome
    {
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();
        public Gene[] Genes { get; set; }
        public List<int> switchingPoint { get; set; }

        public Chromosome()
        {
            switchingPoint = GetSwitchingPoints();
        }

        private static List<int> GetSwitchingPoints()
        {
            lock (SyncLock)
            {
                int Min = 1;
                int Max = 10;
                List<int> switchingPoints = Enumerable
                    .Repeat(0, Random.Next(3))
                    .Select(i => Random.Next(Min, Max))
                    .ToList();
                return switchingPoints;
            }
        }

        public Chromosome GenerateChromosome(int length)
        {
            Genes = new Gene[length];
            Genes[0] = Gene.GetStartGene();
            // if you can minimize this pls do - I don tire
            // -------------------------------------------------------
            // it creates the random genes based on the chromosomes switching points
            if (switchingPoint.Count == 0)
            {
                for (int i = 1; i < Genes.Length - 1; i++)
                {
                    Genes[i] = Gene.GenerateRandomMonotoneGene(i + 1, Location.Locus);
                }
            }
            else
            {
                switchingPoint.Sort();
                for (int i = 1; i < Genes.Length - 1; i++)
                {
                    if (i < switchingPoint[0])
                    {
                        Genes[i] = Gene.GenerateRandomMonotoneGene(i + 1, Location.Locus);
                    }
                    else
                    {
                        if (switchingPoint.Count > 1 && i >= switchingPoint[1])
                        {
                            Genes[i] = Gene.GenerateRandomMonotoneGene(i + 1, Location.Locus);
                        }
                        else
                        {
                            Genes[i] = Gene.GenerateRandomMonotoneGene(i + 1, Location.Allele);
                        }
                    }
                }
            }

            // ---------------------------------------------------------------------------------
            Genes[length - 1] = Gene.GetGoalGene();
            return this;
        }

        public override string ToString()
        {
            StringBuilder chromosome = new StringBuilder();
            chromosome.Append("Chromosome: genes - \n");
            int j = 1;
            foreach (var gene in Genes)
            {
                chromosome.Append($"\t {j}-{gene}\n");
                j++;
            }

            chromosome.Append("switching point - ");
            foreach (var i in switchingPoint)
            {
                chromosome.Append($"{i} ,");
            }

            return chromosome.ToString();
        }
    }
}