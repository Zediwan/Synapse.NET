namespace Synapse.NET.Models;

public static class InnovationCodex
{
    private static readonly Dictionary<string, int> NodeInnovationKeys = [];
    private static readonly Dictionary<string, int> ConnectionInnovationKeys = [];

    public static int NextNodeInnovationId = 1;
    public static int NextConnectionInnovationId = 1;

    public static int GetOrCreateInnovationIdForConnection(NodeGene fromNode, NodeGene toNode)
    {
        var key = $"{fromNode.InnovationId}->{toNode.InnovationId}";

        if (ConnectionInnovationKeys.TryGetValue(key, out var id))
            return id;

        id = NextConnectionInnovationId++;
        ConnectionInnovationKeys[key] = id;
        return id;
    }

    public static int GetOrCreateInnovationIdForSplitNode(ConnectionGene connection)
    {
        var key = $"Split: {connection.GetKey()}";
        if (ConnectionInnovationKeys.TryGetValue(key, out var id))
            return id;

        var innovationId = NextConnectionInnovationId++;
        ConnectionInnovationKeys[key] = innovationId;
        return innovationId;
    }
}