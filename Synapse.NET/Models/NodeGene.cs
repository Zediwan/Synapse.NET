namespace Synapse.NET.Models;

public class NodeGene(NeuronType type, ActivationType activationType = ActivationType.Sigmoid, double bias = 0.0, bool enabled = true)
{
    public Guid Id { get; } = Guid.NewGuid();
    public NeuronType Type { get; } = type;
    public double Bias { get; set; } = bias;
    public bool Enabled { get; set; } = enabled;

    public ActivationType ActivationType { get; set; } = activationType;

    public static double RandomWeight(double min = -1.0, double max = 1.0)
    {
        return min + (max - min) * Random.Shared.NextDouble();
    }
}
