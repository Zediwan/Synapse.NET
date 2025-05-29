using Synapse.NET.Helpers;
using Synapse.NET.Models;

namespace Synapse.NET.Tests.Models;

public class ActivationFunctionTests
{
    [Fact]
    public void ActivationFunctions_ShouldContainAllDefinedFunctions()
    {
        // Arrange
        var activationTypes = Enum.GetValues<ActivationType>();
        // Act
        var actualFunctions = ActivationFunctions.Functions.Keys;
        // Assert
        foreach (var activationType in activationTypes)
        {
            Assert.Contains(activationType, actualFunctions);
        }
    }
}
