//using Synapse.NET.Helpers;
//using Synapse.NET.Models;

//namespace Synapse.NET.Tests.Models;

//public class NetworkTest
//{
//    public Network Network;
//    public const int NumInputs = 2;
//    public const int NumOutputs = 1;

//    public Node InputNode1 = new(bias: 0.0, activationFunction: ActivationFunctions.Linear);
//    public Node InputNode2 = new(bias: 0.0, activationFunction: ActivationFunctions.Linear);
//    public Node OutputNode = new(bias: 0.0, activationFunction: ActivationFunctions.Linear);

//    public NetworkTest()
//    {
//        Network = new Network(NumInputs, NumOutputs)
//        {
//            InputNodes =
//            {
//                [0] = InputNode1,
//                [1] = InputNode2
//            },
//            OutputNodes =
//            {
//                [0] = OutputNode
//            }
//        };
//        Network.FullyConnectInputAndOutputNodes();
//    }

//    #region Constructor



//    #endregion

//    #region FullyConnectInputAndOutputNodes

//    [Fact]
//    public void FullyConnectInputAndOutputNodes_ShouldCreateConnectionsWithDefaultWeights()
//    {
//        // Arrange
//        const int numInputs = 2;
//        const int numOutputs = 1;
//        Node inputNode1 = new(bias: 0.0, activationFunction: ActivationFunctions.Linear);
//        Node inputNode2 = new(bias: 0.0, activationFunction: ActivationFunctions.Linear);
//        Node outputNode1 = new(bias: 0.0, activationFunction: ActivationFunctions.Linear);
//        Network network = new(numInputs, numOutputs)
//        {
//            InputNodes =
//            {
//                [0] = inputNode1,
//                [1] = inputNode2
//            },
//            OutputNodes =
//            {
//                [0] = outputNode1
//            }
//        };
//        // Act
//        network.FullyConnectInputAndOutputNodes();
//        // Assert
//        Assert.True(inputNode1.IsGoingTo(outputNode1));
//        Assert.True(outputNode1.IsRecievingFrom(inputNode1));
//        Assert.True(inputNode2.IsGoingTo(outputNode1));
//        Assert.True(outputNode1.IsRecievingFrom(inputNode2));
//    }

//    [Fact]
//    public void FullyConnectInputAndOutputNodes_ShouldCreateConnectionsWithSpecifiedWeights()
//    {
//        // Arrange
//        const int numInputs = 2;
//        const int numOutputs = 1;
//        Node inputNode1 = new(bias: 0.0, activationFunction: ActivationFunctions.Linear);
//        Node inputNode2 = new(bias: 0.0, activationFunction: ActivationFunctions.Linear);
//        Node outputNode1 = new(bias: 0.0, activationFunction: ActivationFunctions.Linear);
//        Network network = new(numInputs, numOutputs)
//        {
//            InputNodes =
//            {
//                [0] = inputNode1,
//                [1] = inputNode2
//            },
//            OutputNodes =
//            {
//                [0] = outputNode1
//            }
//        };
//        var weights = new double[numInputs, numOutputs];
//        weights[0, 0] = 1.0; // Weight from inputNode1 to outputNode1
//        weights[1, 0] = 2.0; // Weight from inputNode2 to outputNode1

//        // Act
//        network.FullyConnectInputAndOutputNodes(weights);
//        // Assert
//        Assert.True(inputNode1.IsGoingTo(outputNode1));
//        Assert.True(outputNode1.IsRecievingFrom(inputNode1));
//        Assert.True(inputNode2.IsGoingTo(outputNode1));
//        Assert.True(outputNode1.IsRecievingFrom(inputNode2));
//        // Check weights
//        Assert.Equal(1.0, inputNode1.getConnectionTo(outputNode1).Weight);
//        Assert.Equal(2.0, inputNode2.getConnectionTo(outputNode1).Weight);
//    }

//    #endregion

//    #region FeedForward

//    [Theory]
//    [InlineData(0.0, 0.0, 0.0)] // No inputs
//    [InlineData(1.0, 2.0, 3.0)] // Simple case
//    [InlineData(-1.0, -2.0, -3.0)] // Negative inputs
//    [InlineData(1.5, 2.5, 4.0)] // Decimal inputs
//    [InlineData(100.0, 200.0, 300.0)] // Large inputs
//    public void FeedForward_ShouldReturnCorrectOutput(double input1, double input2, double expectedOutput)
//    {
//        // Arrange
//        var inputs = new List<double> { input1, input2 };
//        var expectedOutputs = new List<double> { expectedOutput }; // Expected output is the sum of inputs plus bias (0.0)

//        // Act
//        var outputs = Network.FeedForward(inputs);

//        // Assert
//        Assert.Equal(expectedOutputs.Count, outputs.Count);
//        Assert.Equal(expectedOutputs[0], outputs[0]);
//    }

//    #endregion


//}