using Synapse.NET.Helpers;

namespace Synapse.NET.Models;

public class Node
{
    private double _value;
    public double Value
    {
        get
        {
            if (InConnections is { Count: > 0 })
                _value += InConnections.Sum(c => c.Value);

            return ActivationFunction(_value + Bias);
        }
        set => _value = value;
    }

    public double Bias { get; set; }
    public Func<double, double>  ActivationFunction { get; set; }

    public List<Connection> InConnections = [];
    public List<Connection> OutConnections = [];

    public const double DEFAULT_BIAS = 0.0;
    public static readonly Func<double, double> DEFAULT_ACTIVATION_FUNCTION = ActivationFunctions.Linear;

    public Node(double bias = Node.DEFAULT_BIAS, Func<double, double>? activationFunction = null)
    {
        Bias = bias;
        ActivationFunction = activationFunction ?? Node.DEFAULT_ACTIVATION_FUNCTION;
    }

    /// <summary>
    /// Checks if this node is sending output to another node.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsGoingTo(Node other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return Connection.AreConnected(this, other);
    }

    /// <summary>
    /// Checks if this node is receiving input from another node.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsRecievingFrom(Node other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return Connection.AreConnected(other, this);
    }

    /// <summary>
    /// Gets the connection to another node.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Connection getConnectionTo(Node other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return OutConnections.FirstOrDefault(c => c.To == other) ?? throw new InvalidOperationException($"No connection found to node {other}.");
    }
}