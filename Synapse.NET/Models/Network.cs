namespace Synapse.NET.Models;

public class Network(Genome genome)
{
    private readonly Dictionary<Guid, double> _currentValues = new();

    public void FeedForward(Dictionary<Guid, float> inputValues)
    {
        _currentValues.Clear();

        // Load inputs
        foreach (var (id, value) in inputValues)
            _currentValues[id] = value;

        // Process nodes (topological sort needed for hidden layers)
        foreach (var conn in genome.Connections.Values.Where(c => c.Enabled))
        {
            var input = _currentValues.GetValueOrDefault(conn.FromNode, 0f);
            _currentValues[conn.ToNode] = _currentValues.GetValueOrDefault(conn.ToNode) + input * conn.Weight;
        }

        // Apply activation function
        foreach (var node in genome.Nodes.Values.Where(node => node.Type != NeuronType.Input && node.Type != NeuronType.Bias))
        {
            _currentValues[node.Id] = node.Activation(_currentValues.GetValueOrDefault(node.Id));
        }
    }

    public double GetOutput(Guid outputNode)
    {
        return _currentValues.GetValueOrDefault(outputNode, 0f);
    }
}