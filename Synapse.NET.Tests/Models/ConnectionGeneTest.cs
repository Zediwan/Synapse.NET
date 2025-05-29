using Synapse.NET.Models;

namespace Synapse.NET.Tests.Models;

public class ConnectionGeneTest
{
    [Fact]
    public void ConnectionGene_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var fromNode = new NodeGene(NeuronType.Input, 1, ActivationType.Linear);
        var toNode = new NodeGene(NeuronType.Output, 2, ActivationType.Linear);
        double weight = 0.5;
        bool enabled = true;
        // Act
        var connection = new ConnectionGene(fromNode, toNode, weight, enabled);
        // Assert
        Assert.Equal(fromNode, connection.FromNode);
        Assert.Equal(toNode, connection.ToNode);
        Assert.Equal(weight, connection.Weight);
        Assert.True(connection.Enabled);
        Assert.NotEqual(0, connection.InnovationId); // InnovationId should be set
    }

    [Fact]
    public void ConnectionGene_Constructor_ShouldCreateConnectionWithSameId()
    {
        // Arrange
        var fromNode = new NodeGene(NeuronType.Input, 1, ActivationType.Linear);
        var toNode = new NodeGene(NeuronType.Output, 2, ActivationType.Linear);
        double weight = 0.5;
        bool enabled = true;
        // Act
        var connection1 = new ConnectionGene(fromNode, toNode, weight, enabled);
        var connection2 = new ConnectionGene(fromNode, toNode, weight, enabled);
        // Assert
        Assert.Equal(connection1.InnovationId, connection2.InnovationId); // Should have the same innovation ID
    }
}
