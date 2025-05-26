namespace Synapse.NET.Models;

public class Node(double bias = 0)
{
    private double _value;

    public double Value
    {
        get
        {
            if (InConnections is { Count: > 0 })
                _value += InConnections.Sum(c => c.Value);

            return _value + Bias;
        }
        set => _value = value;
    }

    public double Bias { get; set; } = bias;
    public List<Connection> InConnections = [];
    public List<Connection> OutConnections = [];
}