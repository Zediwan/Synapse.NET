using Synapse.NET.Helpers;
using Synapse.NET.Models;

namespace Synapse.NET.Tests.Models;

public class NetworkTest
{
    public Network Network;
    public const int NumInputs = 2;
    public const int NumOutputs = 1;

    public Node inputNode1 = new Node(bias: 0.0, activationFunction: ActivationFunctions.Linear);
    public Node inputNode2 = new Node(bias: 0.0, activationFunction: ActivationFunctions.Linear);
    public Node outputNode = new Node(bias: 0.0, activationFunction: ActivationFunctions.Linear);

    public NetworkTest()
    {
        Network = new Network(NumInputs, NumOutputs)
        {
            InputNodes =
            {
                [0] = inputNode1,
                [1] = inputNode2
            },
            OutputNodes =
            {
                [0] = outputNode
            }
        };
        Network.FullyConnectNodes();
    }


    #region Constructor



    #endregion

    #region FeedForward

    [Theory]
    [InlineData(0.0, 0.0, 0.0)] // No inputs
    [InlineData(1.0, 2.0, 3.0)] // Simple case
    [InlineData(-1.0, -2.0, -3.0)] // Negative inputs
    [InlineData(1.5, 2.5, 4.0)] // Decimal inputs
    [InlineData(100.0, 200.0, 300.0)] // Large inputs
    public void FeedForward_ShouldReturnCorrectOutput(double input1, double input2, double expectedOutput)
    {
        // Arrange
        var inputs = new List<double> { input1, input2 };
        var expectedOutputs = new List<double> { expectedOutput }; // Expected output is the sum of inputs plus bias (0.0)

        // Act
        var outputs = Network.FeedForward(inputs);

        // Assert
        Assert.Equal(expectedOutputs.Count, outputs.Count);
        Assert.Equal(expectedOutputs[0], outputs[0]);
    }

    #endregion


}