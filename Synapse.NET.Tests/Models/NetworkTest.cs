using Synapse.NET.Models;

namespace Synapse.NET.Tests.Models;

public class NetworkTest
{
    [Fact]
    public void Network_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var genome = GenomeFactory.CreateInitialGenome(new GenomeConfig
        {
            NumInputs = 2,
            NumOutputs = 1,
            UseBias = true
        });
        // Act
        var network = new Network(genome);
        // Assert
        Assert.NotNull(network);
    }

    [Fact]
    public void Network_Forward_ShouldReturnCorrectOutput()
    {
        // Arrange
        var genome = GenomeFactory.CreateInitialGenome(new GenomeConfig
        {
            NumInputs = 1,
            NumOutputs = 1,
            UseBias = true
        });

        var network = new Network(genome);

        // Set input values
        var inputs = new Dictionary<int, float>
        {
            { 1, 0.5f }, // Input node ID 1
            { 2, 0.8f }  // Input node ID 2
        };
        // Act
        network.FeedForward(inputs);
        var output = network.GetOutput(1);
        // Assert

    }
}
