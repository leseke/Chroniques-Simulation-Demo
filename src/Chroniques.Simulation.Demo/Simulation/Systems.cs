using Chroniques.Simulation.Demo.Core;

namespace Chroniques.Simulation.Demo.Simulation;

public sealed class CommunityEventSystem : ISimulationSystem
{
    public void Execute(SimulationWorld world)
    {
        if (world.Event is null && world.Agents.Count(a => a.ResourceLevel <= 20) >= 2)
        {
            world.Event = new CommunityEvent { Type = "demo.community-resource-distress" };
            foreach (var agent in world.Agents.Where(a => a.ResourceLevel <= 20))
                agent.HasDirectKnowledge = true;
        }

        if (world.Event is { IsResolved: false } && world.Agents.Where(a => a.HasDirectKnowledge).All(a => a.ResourceLevel > 20))
            world.Event.IsResolved = true;
    }
}

public sealed class ProgressiveRumorSystem : ISimulationSystem
{
    public void Execute(SimulationWorld world)
    {
        if (world.Event is null)
            return;

        if (!world.Agents.Any(a => a.HoldsRumor))
        {
            var first = world.Agents.First(a => a.HasDirectKnowledge);
            first.HoldsRumor = true;
            return;
        }

        var next = world.Agents
            .Where(a => !a.HoldsRumor)
            .OrderBy(a => a.Id)
            .FirstOrDefault();

        if (next is not null)
            next.HoldsRumor = true;
    }
}

public sealed class MemoryEvolutionSystem : ISimulationSystem
{
    public void Execute(SimulationWorld world)
    {
        if (world.Event is { IsResolved: true } && world.Memory is null)
            world.Memory = new WorldMemory { Type = "demo.community-resource-distress" };

        if (world.Memory is null)
            return;

        world.Memory.Tier = world.Generation switch
        {
            >= 3 => MemoryTier.Legend,
            >= 1 => MemoryTier.Memory,
            _ => MemoryTier.Anecdote,
        };
    }
}

public static class DemoScenario
{
    public static (SimulationWorld World, Scheduler Scheduler) Create(int seed = 42)
    {
        var world = new SimulationWorld(seed);
        world.Agents.AddRange([
            new Agent(1, "A") { ResourceLevel = 10 },
            new Agent(2, "B") { ResourceLevel = 20 },
            new Agent(3, "C") { ResourceLevel = 100 },
        ]);

        var scheduler = new Scheduler()
            .Register(new CommunityEventSystem())
            .Register(new ProgressiveRumorSystem())
            .Register(new MemoryEvolutionSystem());

        return (world, scheduler);
    }

    public static string Trace(SimulationWorld world)
        => string.Join("|", new[]
        {
            $"tick={world.Tick}",
            $"event={world.Event?.Type ?? "none"}",
            $"resolved={world.Event?.IsResolved ?? false}",
            $"direct={string.Join(',', world.Agents.Where(a => a.HasDirectKnowledge).Select(a => a.Id))}",
            $"rumor={string.Join(',', world.Agents.Where(a => a.HoldsRumor).Select(a => a.Id))}",
            $"memory={world.Memory?.Tier.ToString() ?? "none"}",
        });
}
