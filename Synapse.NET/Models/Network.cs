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

    public void FullyConnectNodes()
    {
        if (InputNodes.Count != NumInputs)
            throw new InvalidOperationException($"Input nodes count ({InputNodes.Count}) does not match NumInputs ({NumInputs}).");

        if (OutputNodes.Count != NumOutputs)
            throw new InvalidOperationException($"Output nodes count ({OutputNodes.Count}) does not match NumOutputs ({NumOutputs}).");

        foreach (var connection in from inputNode in InputNodes from outputNode in OutputNodes select new Connection(inputNode, outputNode, 1.0)) {}
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