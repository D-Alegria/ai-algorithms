using System.Text;

namespace Core
{
    public struct Chromosome
    {
        public Gene[] Genes { get; }
        public int[] PathSwitch { get; set; }

        public Chromosome(Gene[] genes, int[] pathSwitch)
        {
            Genes = genes;
            PathSwitch = pathSwitch;
        }

        public override string ToString()
        {
            StringBuilder chromosome = new StringBuilder();
            foreach (var gene in Genes)
            {
                chromosome.Append($"{gene}\n");
            }

            chromosome.Append($"Path Switch: {string.Join(",", PathSwitch)}\n");

            return chromosome.ToString();
        }
    }
}