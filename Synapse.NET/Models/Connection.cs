using Synapse.NET.Helpers;

namespace Synapse.NET.Models;

public class Connection
{
    public double Value => ActivationFunction(From.Value * Weight);
    public double Weight { get; set; }
    public Func<double, double> ActivationFunction { get; set; }
    public Node From { get; set; }
    public Node To { get; set; }

    public Connection(Node from, Node to, double weight = 1.0, Func<double, double>? activationFunction = null)
    {
        From = from ?? throw new ArgumentNullException(nameof(from));
        To = to ?? throw new ArgumentNullException(nameof(to));
        Weight = weight;
        ActivationFunction = activationFunction ?? ActivationFunctions.Sigmoid;
        // Automatically add this connection to the nodes' connection lists
        From.OutConnections.Add(this);
        To.InConnections.Add(this);
    }
}