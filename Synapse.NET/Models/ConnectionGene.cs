namespace Synapse.NET.Models;

public class ConnectionGene(NodeGene fromNode, NodeGene toNode, double weight, bool enabled = true)
{
    public NodeGene FromNode { get; } = fromNode;
    public NodeGene ToNode { get; } = toNode;
    public double Weight { get; set; } = weight;
    public bool Enabled { get; set; } = enabled;
    public int InnovationId { get; } = InnovationCodex.GetOrCreateInnovationIdForConnection(fromNode, toNode); // Unique innovation number for this connection

    public string GetKey() => $"{FromNode}->{ToNode}"; // For alignment without innovation numbers

    public ConnectionGene Clone()
    {
        throw new NotImplementedException();
    }
}