using Synapse.NET.Helpers;
using Synapse.NET.Models;

namespace Synapse.NET.Tests.Models;

public class NodeGeneTest
{
    #region Constructor

    [Fact]
    public void NodeGene_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        const NeuronType type = NeuronType.Hidden;
        const ActivationType activationType = ActivationType.Sigmoid;
        const double bias = 0.5;
        const bool enabled = true;
        const int innovationId = 42;
        // Act
        var nodeGene = new NodeGene(type, activationType, bias: bias, enabled: enabled, innovationId: innovationId);
        // Assert
        Assert.Equal(type, nodeGene.Type);
        Assert.Equal(activationType, nodeGene.ActivationType);
        Assert.Equal(bias, nodeGene.Bias);
        Assert.True(nodeGene.Enabled);
        Assert.Equal(innovationId, nodeGene.InnovationId);
    }

    [Fact]
    public void NodeGene_Constructor_ShouldThrowException_WhenActivationAndTypeAreNull()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new NodeGene(NeuronType.Input, activationType: null, activation: null));
    }

    [Fact]
    public void NodeGene_Constructor_ShouldThrowException_WhenInnovationIdIsNullForHiddenNode()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new NodeGene(NeuronType.Hidden, activationType: ActivationType.Tanh, innovationId: null));
    }

    [Fact]
    public void NodeGene_Constructor_ShouldIgnoreActivationIfActivationTypeIsProvided()
    {
        // Arrange
        const NeuronType type = NeuronType.Output;
        const ActivationType activationType = ActivationType.Linear;
        var expectedActivation = ActivationFunctions.Functions[activationType];
        Func<double, double> customActivation = x => x + 1;
        // Act
        var nodeGene = new NodeGene(type, activationType, activation: customActivation);
        // Assert
        Assert.Equal(type, nodeGene.Type);
        Assert.Equal(activationType, nodeGene.ActivationType);

        // Check that the activation function is the one defined for the activation type
        for (var i = -100; i < 100; i++)
        {
            Assert.Equal(expectedActivation(i), nodeGene.Activation(i));
        }
    }

    [Fact]
    public void NodeGene_Constructor_ShouldUseCustomActivationIfNoActivationTypeIsProvided()
    {
        // Arrange
        const NeuronType type = NeuronType.Input;
        Func<double, double> customActivation = x => x * 2;
        // Act
        var nodeGene = new NodeGene(type, activation: customActivation);
        // Assert
        Assert.Equal(type, nodeGene.Type);
        Assert.Null(nodeGene.ActivationType);
        Assert.Equal(customActivation, nodeGene.Activation);
    }

    #endregion
}