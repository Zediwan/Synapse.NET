namespace Synapse.NET.Models;

public class ConnectionGene(Guid fromNode, Guid toNode, double weight, bool enabled = true)
{
    public Guid FromNode { get; } = fromNode;
    public Guid ToNode { get; } = toNode;
    public double Weight { get; set; } = weight;
    public bool Enabled { get; set; } = enabled;
    public int InnovationId { get; } = InnovationCodex.GetOrCreateInnovationId(InnovationType.Connection, fromNode, toNode); // Unique innovation number for this connection

    public string GetKey() => $"{FromNode}->{ToNode}"; // For alignment without innovation numbers

    public ConnectionGene Clone()
    {
        throw new NotImplementedException();
    }
}