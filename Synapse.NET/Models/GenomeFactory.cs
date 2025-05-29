namespace Synapse.NET.Models;

public static class GenomeFactory
{
    public static Genome CreateInitialGenome(GenomeConfig config)
    {
        var genome = new Genome();

        int idCounter = 1;

        if (config.UseBias)
        {
            genome.AddNode(new NodeGene(NeuronType.Bias, idCounter++, activationType: ActivationType.Linear));
        }

        for (int i = 0; i < config.NumInputs; i++)
        {
            genome.AddNode(new NodeGene(NeuronType.Input, idCounter++, activationType: ActivationType.Linear));
        }

        for (int i = 0; i < config.NumOutputs; i++)
        {
            genome.AddNode(new NodeGene(NeuronType.Output, idCounter++, activationType: ActivationType.Sigmoid));
        }

        // Set the innovation ID counter to the highest ID used
        if (InnovationCodex.NextNodeInnovationId < idCounter)
        {
            InnovationCodex.NextNodeInnovationId = idCounter;
        }

        // Optionally add random connections between inputs/bias → outputs
        // This is done probabilistically in original NEAT
        return genome;
    }
}
