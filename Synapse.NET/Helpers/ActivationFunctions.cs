using Synapse.NET.Models;

namespace Synapse.NET.Helpers;

public static class ActivationFunctions
{
    public static readonly Dictionary<ActivationType, Func<double, double>> Functions =
        new()
        {
            { ActivationType.Sigmoid, x => 1.0 / (1.0 + Math.Exp(-x)) },
            { ActivationType.Tanh, Math.Tanh },
            { ActivationType.ReLU, x => Math.Max(0.0, x) },
            { ActivationType.Linear, x => x },
            { ActivationType.LeakyReLU, x => x > 0 ? x : 0.01 * x },
            { ActivationType.Step, x => x >= 0 ? 1.0 : 0.0 }
        };
}