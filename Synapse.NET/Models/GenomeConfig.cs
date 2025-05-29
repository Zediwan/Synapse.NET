namespace Synapse.NET.Models;

public class GenomeConfig
{
    /// <summary>
    /// Number of Inputs for a <see cref="Genome"/>
    /// </summary>
    public int NumInputs { get; init; }
    /// <summary>
    /// Number of outputs for a <see cref="Genome"/>
    /// </summary>
    public int NumOutputs { get; init; }
    /// <summary>
    /// Indicates if a Node of Type <see cref="NeuronType.Bias"/> should be added
    /// </summary>
    public bool UseBias { get; init; } = true;

    // TODO: implement consideration of this
    /// <summary>
    /// Dictionary of options of <see cref="ActivationType"/> for the Bias Node alongside their probability
    /// </summary>
    public Dictionary<ActivationType, double>? BiasActivationTypes { get; init; } = [];
    // TODO: implement consideration of this
    /// <summary>
    /// Dictionary of options of <see cref="ActivationType"/> for the Input Nodes alongside their probability
    /// </summary>
    public Dictionary<ActivationType, double>? InputActivationTypes { get; init; } = [];
    // TODO: implement consideration of this
    /// <summary>
    /// Dictionary of options of <see cref="ActivationType"/> for the Output Nodes alongside their probability
    /// </summary>
    public Dictionary<ActivationType, double> OutputActivationTypes { get; init; } = [];
}
