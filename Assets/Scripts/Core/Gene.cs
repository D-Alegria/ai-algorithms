using System;

namespace Core
{
    public class Gene
    {
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();
        public int locus { get; private set; }
        public int allele { get; private set; }

        public Gene()
        {
        }

        public Gene(int locus, int allele)
        {
            this.locus = locus;
            this.allele = allele;
        }

        public static Gene GetStartGene()
        {
            return new Gene(1, 1);
        }

        public static Gene GetGoalGene()
        {
            return new Gene(10, 10);
        }

        public static Gene GenerateRandomMonotoneGene(int value, Location location)
        {
            int Min = 1;
            int Max = 10;

            lock (SyncLock)
            {
                switch (location)
                {
                    case Location.Locus:
                        return new Gene(value, Random.Next(Min, Max));
                    case Location.Allele:
                        return new Gene(Random.Next(Min, Max), value);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(location), location, null);
                }
            }
        }

        public override string ToString()
        {
            return $"Gene: x - {locus}, y - {allele}";
        }
    }

    public enum Location
    {
        Locus, // X
        Allele // Y
    }
}