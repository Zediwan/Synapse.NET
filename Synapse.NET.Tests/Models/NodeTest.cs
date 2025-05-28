//using Synapse.NET.Helpers;
//using Synapse.NET.Models;

//namespace Synapse.NET.Tests.Models;

//public class NodeTest
//{
//    #region LinearTestNode
    
//    const double linearTestNodeDefaultValue = 0.0;
//    const double linearTestNodeBias = 0.0;
//    static readonly Func<double, double> linearTestNodeAF = ActivationFunctions.Linear;
//    Node linearTestNode = new Node(bias: linearTestNodeBias,activationFunction: linearTestNodeAF);
    
//    #endregion

//    #region Constructor

//    [Fact]
//    public void Node_InitialValue_ShouldBeZero()
//    {
//        // Assert
//        Assert.Equal(linearTestNodeDefaultValue, linearTestNode.Value);
//        Assert.Equal(linearTestNodeBias, linearTestNode.Bias);
//    }

//    [Fact]
//    public void Node_SetValue_ShouldUpdateValue()
//    {
//        // Arrange
//        const double newValue = 0.5;
//        // Act
//        linearTestNode.Value = newValue;
//        // Assert
//        Assert.Equal(newValue, linearTestNode.Value);
//        Assert.Equal(linearTestNodeBias, linearTestNode.Bias);
//    }

//    [Fact]
//    public void Node_SetBias_ShouldUpdateBias()
//    {
//        // Arrange
//        const double newBias = 2.0;
//        // Act
//        linearTestNode.Bias = newBias;
//        // Assert
//        Assert.Equal(linearTestNodeDefaultValue + newBias, linearTestNode.Value);
//        Assert.Equal(newBias, linearTestNode.Bias);
//    }

//    #endregion

//    #region Value

//    #region Bias

//    [Theory]
//    [InlineData(0.0)]
//    [InlineData(1.0)]
//    [InlineData(-1.0)]
//    public void Node_GetValue_ShouldAddBias(int bias)
//    {
//        // Arrange
//        const double value = 3.0;
//        var expectedValue = value + bias;
//        linearTestNode.Bias = bias;
//        // Act
//        linearTestNode.Value = value;
//        // Assert
//        Assert.Equal(expectedValue, linearTestNode.Value);
//    }

//    #endregion

//    // TODO: add Tests with mocked connections

//    #endregion
//}