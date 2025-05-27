namespace Synapse.NET.Models;

public class Genome
{
    public Dictionary<Guid, NodeGene> Nodes { get; } = new();
    public Dictionary<string, ConnectionGene> Connections { get; } = new();

    public void AddNode(NodeGene node)
    {
        Nodes[node.Id] = node;
    }

    public void AddConnection(ConnectionGene connection)
    {
        var key = connection.GetKey();
        Connections.TryAdd(key, connection);
    }

    public Genome Clone()
    {
        var clone = new Genome();
        foreach (var node in Nodes.Values)
            clone.AddNode(new NodeGene(node.Type)); // fresh ID per birth

        foreach (var conn in Connections.Values)
            clone.AddConnection(new ConnectionGene(conn.FromNode, conn.ToNode, conn.Weight, conn.Enabled));

        return clone;
    }

    #region Mutation

    #region Addition
    public void MutateAddNode()
    {
        // Ensure there are connections to split
        if (Connections.Count == 0) return;

        // Pick a random connection to split
        var conn = Connections.Values.ElementAt(Random.Shared.Next(Connections.Count));
        conn.Enabled = false;

        var newNode = new NodeGene(NeuronType.Hidden);
        AddNode(newNode);

        AddConnection(new ConnectionGene(conn.FromNode, newNode.Id, 1.0f));
        AddConnection(new ConnectionGene(newNode.Id, conn.ToNode, conn.Weight));
    }

    public void MutateAddConnection()
    {
        var inputNodes = Nodes.Values.Where(n => n.Type == NeuronType.Input).ToList();
        var outputNodes = Nodes.Values.Where(n => n.Type != NeuronType.Input && n.Type != NeuronType.Bias).ToList();

        // Ensure there are input and output nodes to connect
        if (inputNodes.Count == 0 || outputNodes.Count == 0) return;

        // Randomly select an input and output node to connect
        var fromNode = inputNodes[Random.Shared.Next(inputNodes.Count)].Id;
        var toNode = outputNodes[Random.Shared.Next(outputNodes.Count)].Id;

        // Avoid self-connections
        if (fromNode == toNode) return;

        AddConnection(new ConnectionGene(fromNode, toNode, Random.Shared.NextSingle() * 2 - 1));
    }

    #endregion

    #region Disabling

    public void MutateDisableNode(NodeGene node)
    {
        if (node.Type is NeuronType.Input or NeuronType.Bias)
            return; // Never remove inputs or bias nodes

        var incoming = Connections.Values.Where(c => c.ToNode == node.Id).ToList();
        var outgoing = Connections.Values.Where(c => c.FromNode == node.Id).ToList();

        // Create shortcuts from each input → output
        // TODO: Depending on settings the weight should be inherited, averaged or random
        foreach (var inC in incoming)
        {
            foreach (var outC in outgoing)
            {
                AddConnection(new ConnectionGene(inC.FromNode, outC.ToNode, NodeGene.RandomWeight()));
            }
        }

        // Disable the node and all related connections
        node.Enabled = false;
        foreach (var c in incoming.Concat(outgoing))
            c.Enabled = false;
    }

    #endregion

    #endregion
}