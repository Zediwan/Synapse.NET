using Synapse.NET.Models;

namespace Synapse.NET.Tests.Models;

public class GenomeFactoryTest
{
    [Fact]
    public void GenomeFactory_CreateGenome_ShouldReturnGenomeWithDefaultValues()
    {
        // Arrange
        GenomeConfig config = new()
        {
            NumInputs = 1,
            NumOutputs = 1,
        };

        // Act
        var genome = GenomeFactory.CreateInitialGenome(config);

        // Assert
        Assert.NotNull(genome);
        Assert.NotEmpty(genome.Nodes);
        Assert.Empty(genome.Connections);
        Assert.Equal(config.NumInputs + (config.UseBias ? 1 : 0), genome.Nodes.Count(n => n.Value.Type == NeuronType.Input || n.Value.Type == NeuronType.Bias));
        Assert.Equal(config.NumOutputs, genome.Nodes.Count(n => n.Value.Type == NeuronType.Output));
        Assert.Equal(config.NumInputs + config.NumOutputs + (config.UseBias ? 1 : 0), genome.Nodes.Count);
        Assert.Equal(InnovationCodex.NextNodeInnovationId, genome.Nodes.Count + 1);
    }

    [Fact]
    public void GenomeFactory_CreateGenome_WithoutBias()
    {
        // Arrange
        GenomeConfig config = new()
        {
            NumInputs = 1,
            NumOutputs = 1,
            UseBias = false,
        };
        // Act
        var genome = GenomeFactory.CreateInitialGenome(config);
        // Assert
        Assert.NotNull(genome);
        Assert.DoesNotContain(genome.Nodes.Values, n => n.Type == NeuronType.Bias);
        Assert.Equal(2, genome.Nodes.Count); // 1 inputs + 1 bias + 1 output
    }

    [Fact]
    public void GenomeFactory_CreateGenome_RepeatedCalls_ShouldReturnSameInnovationId()
    {
        // Arrange
        GenomeConfig config = new()
        {
            NumInputs = 1,
            NumOutputs = 1,
        };
        // Act
        var genome1 = GenomeFactory.CreateInitialGenome(config);
        var genome2 = GenomeFactory.CreateInitialGenome(config);
        // Assert
        Assert.Equal(genome1.Nodes.Count, genome2.Nodes.Count);
        for (int i = 1; i <= genome1.Nodes.Count; i++)
        {
            Assert.Contains(i, genome1.Nodes.Keys);
            Assert.Contains(i, genome2.Nodes.Keys);
        }
    }
}
