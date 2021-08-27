using System;
using System.Collections.Generic;

namespace Core
{
    public struct Gene
    {
        public (int locus, int allelle) PathLocation { get; }
        public PathDirection PathDirection { get; }

        public Gene(( int, int ) pathLocation, PathDirection pathDirection)
        {
            PathLocation = pathLocation;
            PathDirection = pathDirection;
        }

        public override String ToString()
        {
            return
                $"Gene: locus - {PathLocation.locus}, allele - {PathLocation.allelle}," +
                $" path direction - {PathDirection}";
        }
    }

    public enum PathDirection
    {
        Vertical,
        Horizontal
    }
}