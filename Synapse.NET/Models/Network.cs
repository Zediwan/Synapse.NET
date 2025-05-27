using System.Numerics;

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

    /// <summary>
    /// Connects all input nodes to all output nodes with the specified weights.
    /// If no weights are provided, the default weight of 1.0 is used for all connections.
    /// </summary>
    /// <param name="weights"> A 2-dimensional matrix representing the weights (sizes must match) </param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void FullyConnectInputAndOutputNodes(double[,] weights = null)
    {
        if (InputNodes.Count != NumInputs)
            throw new InvalidOperationException($"Input nodes count ({InputNodes.Count}) does not match NumInputs ({NumInputs}).");

        if (OutputNodes.Count != NumOutputs)
            throw new InvalidOperationException($"Output nodes count ({OutputNodes.Count}) does not match NumOutputs ({NumOutputs}).");

        if (weights != null && (weights.GetLength(0) != NumInputs || weights.GetLength(1) != NumOutputs))
            throw new ArgumentException($"Weights matrix dimensions ({weights.GetLength(0)}x{weights.GetLength(1)}) do not match network dimensions ({NumInputs}x{NumOutputs}).", nameof(weights));

        for (var i = 0; i < NumInputs; i++)
        {
            for (var j = 0; j < NumOutputs; j++)
            {
                if (InputNodes[i] == null)
                    throw new InvalidOperationException($"Input node at index {i} is null.");
                if (OutputNodes[j] == null)
                    throw new InvalidOperationException($"Output node at index {j} is null.");

                // Check if the nodes are already connected
                if (InputNodes[i].IsGoingTo(OutputNodes[j]))
                    continue; // Skip if already connected

                var weight = weights != null ? weights[i, j] : 1.0;
                Connection.Connect(InputNodes[i], OutputNodes[j], weight);
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