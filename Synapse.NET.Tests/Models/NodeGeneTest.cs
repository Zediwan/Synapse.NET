using Synapse.NET.Models;

namespace Synapse.NET.Tests.Models;

public class NodeGeneTest
{
    [Fact]
    public void NodeGene_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var innovationId = InnovationCodex.NextNodeInnovationId;
        var activationType = ActivationType.Linear;
        var node = new NodeGene(
            NeuronType.Input,
            innovationId,
            activationType: activationType,
            bias: 0.5,
            enabled: true
        );
        // Act & Assert
        Assert.Equal(NeuronType.Input, node.Type);
        Assert.Equal(innovationId, node.InnovationId);
        Assert.Equal(activationType, node.ActivationType);
        Assert.Equal(0.5, node.Bias);
        Assert.True(node.Enabled);
        Assert.Equal(0.5, node.Activation(0.5)); // Linear activation should return input as is

    }

    [Fact]
    public void NodeGene_Constructor_ShouldActivationTypeOverrideActivation()
    {
        // Arrange
        var activation = new Func<double, double>(x => x * 2);
        var node = new NodeGene(
            NeuronType.Hidden,
            InnovationCodex.NextNodeInnovationId,
            activationType: ActivationType.Linear,
            activation: activation
        );
        // Act
        var result = node.Activation(0.5);
        // Assert
        Assert.Equal(0.5, result);
    }

    [Fact]
    public void NodeGene_Constructor_ShouldThrowIfNeitherActivationTypeNorActivationProvided()
    {
        Assert.Throws<ArgumentException>(() => new NodeGene(NeuronType.Input, InnovationCodex.NextNodeInnovationId));
    }

    [Fact]
    public void NodeGene_Constructor_ShouldUseActivationIfProvidedAndNoActivationType()
    {
        // Arrange
        Func<double, double> customActivation = x => x * 2;
        var node = new NodeGene(
            NeuronType.Hidden,
            InnovationCodex.NextNodeInnovationId,
            activation: customActivation
        );
        // Act
        var result = node.Activation(0.5);
        // Assert
        Assert.Equal(1.0, result); // 0.5 * 2 = 1.0
    }
}
