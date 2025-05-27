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
}