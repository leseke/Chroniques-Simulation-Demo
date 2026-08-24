namespace Chroniques.Simulation.Demo.Core;

public sealed record Agent(int Id, string Name)
{
    public int ResourceLevel { get; set; } = 100;
    public bool HasDirectKnowledge { get; set; }
    public bool HoldsRumor { get; set; }
}

public sealed class CommunityEvent
{
    public required string Type { get; init; }
    public bool IsResolved { get; set; }
}

public enum MemoryTier
{
    None = 0,
    Anecdote = 1,
    Memory = 2,
    Legend = 3,
}

public sealed class WorldMemory
{
    public required string Type { get; init; }
    public MemoryTier Tier { get; set; } = MemoryTier.Anecdote;
}

public sealed class SimulationWorld
{
    public SimulationWorld(int seed)
    {
        Seed = seed;
        Random = new Random(seed);
    }

    public int Seed { get; }
    public int Tick { get; set; }
    public int Generation { get; set; }
    public Random Random { get; }
    public List<Agent> Agents { get; } = [];
    public CommunityEvent? Event { get; set; }
    public WorldMemory? Memory { get; set; }
}

public interface ISimulationSystem
{
    void Execute(SimulationWorld world);
}

public sealed class Scheduler
{
    private readonly List<ISimulationSystem> _systems = [];

    public Scheduler Register(ISimulationSystem system)
    {
        _systems.Add(system);
        return this;
    }

    public void Tick(SimulationWorld world)
    {
        foreach (var system in _systems)
            system.Execute(world);

        world.Tick++;
    }
}
