using Synapse.NET.Helpers;
using Synapse.NET.Models;

namespace Synapse.NET.Tests.Models;

public class ConnectionTest
{
    #region Constructor

    [Fact]
    public void Connection_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var fromNode = new Node(bias: 0.0) { Value = 1.0 };
        var toNode = new Node(bias: 0.0);
        double weight = 2.0;
        // Act
        var connection = new Connection(fromNode, toNode, weight);
        // Assert
        Assert.Equal(fromNode, connection.From);
        Assert.Equal(toNode, connection.To);
        Assert.Equal(weight, connection.Weight);
    }

    #endregion

    #region Value

    [Theory]
    [InlineData(1.0, 2.0, 2.0)]
    [InlineData(-1.0, 2.0, -2.0)]
    [InlineData(0.0, 2.0, 0.0)]
    public void Connection_Value_ShouldBeCalculatedCorrectly(double weight, double fromValue, double expectedValue)
    {
        // Arrange
        var fromNode = new Node(bias: 0.0, activationFunction: ActivationFunctions.Linear) { Value = fromValue };
        var toNode = new Node(bias: 0.0, activationFunction: ActivationFunctions.Linear);
        var connection = new Connection(fromNode, toNode, weight);
        // Act
        var actualValue = connection.Value;
        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    #endregion
}