using Synapse.NET.Helpers;

namespace Synapse.NET.Models;

public class Connection
{
    public double Value => From.Value * Weight;
    public double Weight { get; set; }
    public Node From { get; set; }
    public Node To { get; set; }

    public Connection(Node from, Node to, double weight = 1.0)
    {
        From = from ?? throw new ArgumentNullException(nameof(from));
        To = to ?? throw new ArgumentNullException(nameof(to));
        Weight = weight;

        // Automatically add this connection to the nodes' connection lists
        From.OutConnections.Add(this);
        To.InConnections.Add(this);
    }

    /// <summary>
    /// Creates a connection between two nodes with the specified weight.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public static Connection Connect(Node from, Node to, double weight = 1.0)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);

        return new Connection(from, to, weight);
    }

    /// <summary>
    /// Checks if two nodes are connected, either directly or indirectly.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static bool AreConnected(Node from, Node to)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);
        return from.OutConnections.Any(c => c.To == to) || to.InConnections.Any(c => c.From == from);
    }
}