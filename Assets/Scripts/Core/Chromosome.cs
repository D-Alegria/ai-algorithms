using System.Text;

namespace Core
{
    public readonly struct Chromosome
    {
        public Gene[] Genes { get; }
        public bool IsColumnWise { get; }
        public int[] PathSwitch { get; }
        
        public Chromosome(Gene[] genes, bool isColumnWise, int[] pathSwitch)
        {
            Genes = genes;
            IsColumnWise = isColumnWise;
            PathSwitch = pathSwitch;
        }

        public override string ToString()
        {
            StringBuilder chromosome = new StringBuilder();
            foreach (var gene in Genes)
            {
                chromosome.Append($"{gene}\n");
            }

            chromosome.Append($"Is Column-wise: {IsColumnWise}\n");
            chromosome.Append($"Path Switch: {string.Join(",", PathSwitch)}\n");

            return chromosome.ToString();
        }
    }
}