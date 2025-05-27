using Synapse.NET.Helpers;

namespace Synapse.NET.Models;

public class Network
{
    public readonly int NumInputs;
    public readonly int NumOutputs;

    public List<Node> InputNodes;
    public List<Node> OutputNodes;

    public Network(int numInputs, int numOutputs)
    {
        if (numInputs <= 0)
            throw new ArgumentException("Number of inputs must be greater than zero.", nameof(numInputs));
        if (numOutputs <= 0)
            throw new ArgumentException("Number of outputs must be greater than zero.", nameof(numOutputs));

        NumInputs = numInputs;
        NumOutputs = numOutputs;

        InputNodes = Enumerable.Range(0, numInputs).Select(_ => new Node()).ToList();
        OutputNodes = Enumerable.Range(0, numOutputs).Select(_ => new Node()).ToList();
    }

    public void FullyConnect()
    {
        foreach (var inputNode in _inputNodes)
        {
            foreach (var outputNode in _outputNodes)
            {
                var connection = new Connection(inputNode, outputNode, 1.0); // Default weight of 1.0
                inputNode.OutConnections.Add(connection);
                outputNode.InConnections.Add(connection);
            }
        }
    }

    public List<double> FeedForward(List<double> inputs)
    {
        if (inputs.Count != NumInputs)
            throw new ArgumentException($"Expected {NumInputs} inputs, but got {inputs.Count}.", nameof(inputs));

        // Set input node values
        for (var i = 0; i < NumInputs; i++)
        {
            InputNodes[i].Value = inputs[i];
        }

        // Calculate output node values
        var outputs = new List<double>(NumOutputs);
        outputs.AddRange(OutputNodes.Select(outputNode => outputNode.Value));

        return outputs;
    }
}