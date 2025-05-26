using Synapse.NET.Helpers;
using Synapse.NET.Models;

namespace Synapse.NET.Tests.Models;

public class NodeTest
{
    #region Constructor

    [Fact]
    public void Node_InitialValue_ShouldBeZero()
    {
        // Act
        var node = new Node();
        // Assert
        Assert.Equal(0, node.Value);
        Assert.Equal(0, node.Bias);
    }

    [Fact]
    public void Node_SetValue_ShouldUpdateValue()
    {
        // Act
        var node = new Node
        {
            Value = 5.0
        };
        // Assert
        Assert.Equal(5.0, node.Value);
        Assert.Equal(0, node.Bias);
    }

    [Fact]
    public void Node_SetBias_ShouldUpdateBias()
    {
        // Act
        var node = new Node(bias: 2.0);
        // Assert
        Assert.Equal(2.0, node.Bias);
    }

    #endregion

    #region Value

    #region Bias

    [Theory]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(-1.0)]
    public void Node_GetValue_ShouldAddBias(int bias)
    {
        // Arrange
        const double value = 3.0;
        var expectedValue = value + bias;
        // Act
        var node = new Node(bias: bias)
        {
            Value = value
        };
        // Assert
        Assert.Equal(expectedValue, node.Value);
    }

    #endregion

    #region Connection

    // Create a test with mocked connections
    [Fact]
    public void Node_GetValue_ShouldSumInConnections()
    {
        // Arrange
        var node = new Node(bias: 0.0);
        var connection1 = new Connection(new Node(bias:0.0) { Value = 1.0 }, node, 1.0, ActivationFunctions.Linear);
        var connection2 = new Connection(new Node(bias: 0.0) { Value = 1.0 }, node, 1.0, ActivationFunctions.Linear);
        var expectedValue = connection1.Value + connection2.Value;

        // Assert
        Assert.Equal(expectedValue, node.Value);
    }

    #endregion

    #endregion

}