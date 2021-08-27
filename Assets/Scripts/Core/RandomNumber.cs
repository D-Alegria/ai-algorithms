using System;

namespace Core
{
    public static class RandomNumber
    {
        private static readonly Random Generator = new Random();

       public static int Generate(int maxValue)
        {
            return Generator.Next(maxValue);
        }

        public static int Generate(int minValue, int maxValue)
        {
            return Generator.Next(minValue, maxValue);
        }
    }
}