namespace Synapse.NET.Models;

public static class InnovationCodex
{
    private static readonly Dictionary<string, int> InnovationKeys = new();
    private static int _nextInnovationId = 1;

    /// <summary>
    /// Gets or creates a unique innovation ID for a structural mutation,
    /// such as adding a connection or splitting a connection into a new node.
    /// </summary>
    /// <param name="type"> The <see cref="InnovationType"/> </param>
    /// <param name="from"> The start node ID involved in the mutation </param>
    /// <param name="to"> The end node ID involved in the mutation </param>
    /// <returns> A consistent innovation ID for the given mutation type and node pair </returns>
    public static int GetOrCreateInnovationId(InnovationType type, Guid from, Guid to)
    {
        var key = $"{nameof(type)}:{from}->{to}";

        if (InnovationKeys.TryGetValue(key, out var id))
            return id;

        id = _nextInnovationId++;
        InnovationKeys[key] = id;
        return id;
    }
}

/// <summary>
/// Represents the type of innovation being performed in the genome.
/// </summary>
public enum InnovationType
{
    /// <summary>
    /// Adding a new connection between two existing nodes.
    /// </summary>
    Connection,
    /// <summary>
    /// Splitting an existing connection into a new node.
    /// </summary>
    Split
}