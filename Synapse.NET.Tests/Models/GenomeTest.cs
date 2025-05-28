using Synapse.NET.Models;

namespace Synapse.NET.Tests.Models;

public class GenomeTest
{
    #region Constructor

    [Fact]
    public void Genome_Should_Initialize_With_Default_Values()
    {
        // Arrange & Act
        var genome = new Genome();
        // Assert
        Assert.NotNull(genome.Nodes);
        Assert.NotNull(genome.Connections);
        Assert.Empty(genome.Nodes);
        Assert.Empty(genome.Connections);
    }

    [Fact]
    public void Genome_Should_Add_Node_Correctly()
    {
        // Arrange
        var genome = new Genome();
        var node = new NodeGene(NeuronType.Input);
        // Act
        genome.AddNode(node);
        // Assert
        Assert.Single(genome.Nodes);
        Assert.Contains(node.Id, genome.Nodes.Keys);
    }

    [Fact]
    public void Genome_Should_Add_Connection_Correctly()
    {
        // Arrange
        var genome = new Genome();
        var fromNode = new NodeGene(NeuronType.Input);
        var toNode = new NodeGene(NeuronType.Output);
        genome.AddNode(fromNode);
        genome.AddNode(toNode);
        var connection = new ConnectionGene(fromNode.Id, toNode.Id, 0.5f);
        // Act
        genome.AddConnection(connection);
        // Assert
        Assert.Single(genome.Connections);
        Assert.Contains(connection.GetKey(), genome.Connections.Keys);
    }

    #endregion

    #region Clone

    [Fact]
    public void Genome_Should_Clone_Correctly()
    {
        // Arrange
        var genome = new Genome();
        var node = new NodeGene(NeuronType.Input);
        genome.AddNode(node);
        var connection = new ConnectionGene(node.Id, Guid.NewGuid(), 0.5f);
        genome.AddConnection(connection);

        // Act
        var clone = genome.Clone();

        // Assert
        Assert.NotSame(genome, clone);
        Assert.Single(clone.Nodes);
        Assert.Single(clone.Connections);
        // Check if the connection is cloned correctly
        var clonedConnection = clone.Connections.Values.First();
        Assert.Equal(connection.Weight, clonedConnection.Weight);
        Assert.Equal(connection.FromNode, clonedConnection.FromNode);
        Assert.Equal(connection.ToNode, clonedConnection.ToNode);
        Assert.Equal(connection.GetKey(), clonedConnection.GetKey());
        Assert.Equal(connection.Enabled, clonedConnection.Enabled);
        // Check if the node is cloned correctly
        var clonedNode = clone.Nodes.Values.First();
        Assert.Equal(node.Type, clonedNode.Type);
        Assert.Equal(node.ActivationType, clonedNode.ActivationType);
        Assert.Equal(node.Bias, clonedNode.Bias);
        Assert.Equal(node.Enabled, clonedNode.Enabled);
        Assert.NotEqual(node.Id, clonedNode.Id);
    }

    #endregion

    #region Mutation

    #region Addition

    [Fact]
    public void Genome_Should_Mutate_Add_Node()
    {
        // Arrange
        var genome = new Genome();
        var inputNode = new NodeGene(NeuronType.Input);
        var outputNode = new NodeGene(NeuronType.Output);
        genome.AddNode(inputNode);
        genome.AddNode(outputNode);
        var connection = new ConnectionGene(inputNode.Id, outputNode.Id, 0.5f);
        genome.AddConnection(connection);
        // Act
        genome.MutateAddNode();
        // Assert
        Assert.Equal(3, genome.Nodes.Count); // 2 original + 1 new hidden node
        Assert.Equal(3, genome.Nodes.Count(n => n.Value.Enabled)); // All nodes should be enabled
        Assert.Equal(3, genome.Connections.Count); // 1 original connection + 2 new connections
        Assert.Equal(2, genome.Connections.Count(c => c.Value.Enabled)); // Original connection should be disabled and 2 new connections should be enabled
    }

    [Fact]
    public void Genome_Should_Mutate_Add_Connection()
    {
        // Arrange
        var genome = new Genome();
        var inputNode = new NodeGene(NeuronType.Input);
        var outputNode = new NodeGene(NeuronType.Output);
        genome.AddNode(inputNode);
        genome.AddNode(outputNode);
        // Act
        genome.MutateAddConnection();
        // Assert
        Assert.Single(genome.Connections); // 0 original connections + 1 new connection
    }

    #endregion

    #region Disabling



    #endregion

    #endregion
}