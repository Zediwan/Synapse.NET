using Synapse.NET.Helpers;

namespace Synapse.NET.Models;

public class NodeGene
{
    /// <summary> Functional type of this node (Input, Hidden, Output, Bias). </summary>
    public NeuronType Type { get; }

    /// <summary> Whether this node is currently active in the network. </summary>
    public bool Enabled { get; set; }

    /// <summary> Bias value applied before activation. Evolves over time. </summary>
    public double Bias { get; set; }

    /// <summary> The activation function type used by this node. </summary>
    public ActivationType? ActivationType { get; set; }

    /// <summary> The activation function used by this node. </summary>
    public Func<double, double> Activation { get; set; }

    /// <summary> Innovation ID used for crossover alignment. </summary>
    public int InnovationId { get; }

    public NodeGene(
        NeuronType type,
        int innovationId,
        ActivationType? activationType = null,
        Func<double, double>? activation = null,
        double bias = 0.0,
        bool enabled = true)
    {
        if (activationType != null)
        {
            var at = (ActivationType) activationType;
            var func = ActivationFunctions.Functions[at];
            Activation = func ?? throw new ArgumentException($"Activation function for {at} is not defined.");
        }
        else if (activation != null)
        {
            Activation = activation;
        }
        else
        {
            throw new ArgumentException("Either activationType or activation function must be provided.");
        }

        ActivationType = activationType;
        Type = type;
        Bias = bias;
        Enabled = enabled;
        InnovationId = innovationId;
    }

    /// <summary>
    /// Creates a shallow copy of this node with a fresh runtime ID.
    /// Used during genome cloning.
    /// </summary>
    public NodeGene Clone()
    {
        throw new NotImplementedException();
    }

    public static double RandomWeight(double min = -1.0, double max = 1.0)
    {
        return min + (max - min) * Random.Shared.NextDouble();
    }
}
