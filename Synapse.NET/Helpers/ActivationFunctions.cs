namespace Synapse.NET.Helpers;

public static class ActivationFunctions
{
    public static readonly Func<double, double> Sigmoid = x => 1 / (1 + Math.Exp(-x));
	public static readonly Func<double, double> ReLU = x => x < 0 ? 0 : x;
	public static readonly Func<double, double> Tanh = Math.Tanh;
	public static readonly Func<double, double> LeakyReLU = x => x < 0 ? 0.01 * x : x;
	public static readonly Func<double, double> Linear = x => x;
}