using System;
using System.Collections.Generic;

namespace Core
{
    public struct Gene
    {
        public (int locus, int allelle) PathLocation { get; set; }
        public PathDirection PathDirection { get; set; }
        public bool IsColumnWise { get; set; }

        public Gene(( int, int ) pathLocation, PathDirection pathDirection, bool isColumnWise)
        {
            PathLocation = pathLocation;
            PathDirection = pathDirection;
            IsColumnWise = isColumnWise;
        }

        public override String ToString()
        {
            return
                $"Gene: locus - {PathLocation.locus}, allele - {PathLocation.allelle}," +
                $" path direction - {PathDirection}, Is Column-wise: {IsColumnWise}";
        }
    }

    public enum PathDirection
    {
        Vertical,
        Horizontal
    }
}