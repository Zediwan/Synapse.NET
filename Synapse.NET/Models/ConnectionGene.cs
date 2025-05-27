namespace Synapse.NET.Models;

public class ConnectionGene(Guid fromNode, Guid toNode, double weight, bool enabled = true)
{
    public Guid FromNode { get; } = fromNode;
    public Guid ToNode { get; } = toNode;
    public double Weight { get; set; } = weight;
    public bool Enabled { get; set; } = enabled;

    public string GetKey() => $"{FromNode}->{ToNode}"; // For alignment without innovation numbers
}