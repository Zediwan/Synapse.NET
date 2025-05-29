namespace Synapse.NET.Models;

public class Genome
{
    public Dictionary<int, NodeGene> Nodes { get; } = [];
    public Dictionary<int, ConnectionGene> Connections { get; } = [];

    #region Helpers

    /// <summary>
    /// Adds a new node to the genome.
    /// </summary>
    /// <param name="node"> The <see cref="NodeGene"/> to be added. </param>
    public void AddNode(NodeGene node)
    {
        Nodes.TryAdd(node.InnovationId, node);
    }

    /// <summary>
    /// Adds a new connection to the genome.
    /// </summary>
    /// <param name="connection"> The <see cref="ConnectionGene"/> to be added. </param>
    public void AddConnection(ConnectionGene connection)
    {
        Connections.TryAdd(connection.InnovationId, connection);
    }
    
    /// <summary>
    /// Creates a deep copy of this genome, including all nodes and connections.
    /// </summary>
    /// <returns> The copied <see cref="Genome"/> object. </returns>
    public Genome Clone()
    {
        throw new NotImplementedException();
    }

    private bool IsValidConnection(ConnectionGene connection)
    {
        var from = connection.FromNode;
        var to = connection.ToNode;

        // Check if the nodes are identical
        if (from == to)
            return false; // Cannot connect a node to itself

        // Check that from is not an output node
        if (from.Type == NeuronType.Output)
            return false;

        // Check that to is not an input node
        if (to.Type == NeuronType.Input)
            return false;

        // TODO: Check that to precedes from in topological order

        return true;
    }

    #endregion

    #region Mutation

    #region Addition

    public NodeGene? MutateAddNode()
    {
        // TODO: should this always add a node if possible or just randomly try
        // var enabledConnections = Connections.Values.Where(c => c.Enabled).ToList();
        if (Connections.Count == 0) return null;

        // Randomly select a connection to split
        var connection = Connections.Values.ElementAt(Random.Shared.Next(Connections.Count));
        if (connection.FromNode == connection.ToNode) return null; // Cannot split self-loop
        if (!connection.Enabled) return null; // Cannot split a disabled connection

        // Create a new node in the middle of the selected connection
        var newNode = new NodeGene(
            NeuronType.Hidden,
            innovationId: InnovationCodex.GetOrCreateInnovationIdForSplitNode(connection),
            activationType: ActivationType.Linear,
            bias: NodeGene.RandomWeight(),
            enabled: true);
        AddNode(newNode);

        // Create two new connections from the original connection to the new node
        // First connection gets the weight 1
        var conn1 = new ConnectionGene(connection.FromNode, newNode, 1, true);
        AddConnection(conn1);
        // Second connection gets the original weight
        var conn2 = new ConnectionGene(newNode, connection.ToNode, connection.Weight, true);
        AddConnection(conn2);

        // Disable the original connection
        connection.Enabled = false;
        return newNode;
    }

    public ConnectionGene? MutateAddConnection()
    {
        if (Nodes.Count < 2) return null; // Cannot add a connection if there are less than 2 nodes

        // Randomly select two nodes to connect
        var fromNode = Nodes.Values.ElementAt(Random.Shared.Next(Nodes.Count));
        var toNode = Nodes.Values.ElementAt(Random.Shared.Next(Nodes.Count));

        // Create the connection gene
        var connection = new ConnectionGene(fromNode, toNode, NodeGene.RandomWeight(), true);

        if (!IsValidConnection(connection))
            return null;

        // If the connection already exists return
        if (Connections.ContainsKey(connection.InnovationId))
            return null;

        // Add the new connection
        AddConnection(connection);

        return connection;
    }

    #endregion

    #region Disabling

    public NodeGene? MutateDisableNode()
    {
        throw new NotImplementedException();
    }

    public ConnectionGene? MutateDisableConnection()
    {
        throw new NotImplementedException();
    }

    #endregion

    #endregion
}