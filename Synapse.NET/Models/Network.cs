namespace Synapse.NET.Models;

public class Network(Genome genome)
{
    private readonly Dictionary<int, double> _currentValues = [];

    public void FeedForward(Dictionary<int, float> inputValues)
    {
        _currentValues.Clear();

        // Load inputs
        foreach (var (id, value) in inputValues)
            _currentValues[id] = value;

        // Process nodes (topological sort needed for hidden layers)
        foreach (var conn in genome.Connections.Values.Where(c => c.Enabled))
        {
            var input = _currentValues.GetValueOrDefault(conn.FromNode.InnovationId, 0f);
            _currentValues[conn.ToNode.InnovationId] = _currentValues.GetValueOrDefault(conn.ToNode.InnovationId) + input * conn.Weight;
        }

        // Apply activation function
        foreach (var node in genome.Nodes.Values.Where(node => node.Type != NeuronType.Input && node.Type != NeuronType.Bias))
        {
            _currentValues[node.InnovationId] = node.Activation(_currentValues.GetValueOrDefault(node.InnovationId));
        }
    }

    public double GetOutput(int outputNode)
    {
        return _currentValues.GetValueOrDefault(outputNode, 0f);
    }
}