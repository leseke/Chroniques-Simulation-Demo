using Chroniques.Simulation.Demo.Core;
using Chroniques.Simulation.Demo.Simulation;

namespace Chroniques.Simulation.Demo.Tests;

public sealed class DeterministicSimulationTests
{
    [Fact]
    public void FirstTick_CreatesCommunityEvent()
    {
        var (world, scheduler) = DemoScenario.Create();
        scheduler.Tick(world);

        Assert.NotNull(world.Event);
        Assert.Equal("demo.community-resource-distress", world.Event!.Type);
        Assert.Equal(2, world.Agents.Count(a => a.HasDirectKnowledge));
    }

    [Fact]
    public void Rumor_PropagatesOneNewHolderPerTick()
    {
        var (world, scheduler) = DemoScenario.Create();

        scheduler.Tick(world);
        Assert.Single(world.Agents.Where(a => a.HoldsRumor));

        scheduler.Tick(world);
        Assert.Equal(2, world.Agents.Count(a => a.HoldsRumor));

        scheduler.Tick(world);
        Assert.Equal(3, world.Agents.Count(a => a.HoldsRumor));
    }

    [Fact]
    public void Recovery_ResolvesEventAndCreatesMemory()
    {
        var (world, scheduler) = DemoScenario.Create();
        scheduler.Tick(world);

        foreach (var agent in world.Agents.Where(a => a.HasDirectKnowledge))
            agent.ResourceLevel = 100;

        scheduler.Tick(world);

        Assert.True(world.Event!.IsResolved);
        Assert.NotNull(world.Memory);
        Assert.Equal(MemoryTier.Anecdote, world.Memory!.Tier);
    }

    [Fact]
    public void LaterGenerations_PromoteMemoryToLegend()
    {
        var (world, scheduler) = DemoScenario.Create();
        scheduler.Tick(world);

        foreach (var agent in world.Agents.Where(a => a.HasDirectKnowledge))
            agent.ResourceLevel = 100;

        scheduler.Tick(world);

        world.Generation = 3;
        scheduler.Tick(world);

        Assert.Equal(MemoryTier.Legend, world.Memory!.Tier);
    }

    [Fact]
    public void Replay_WithSameSeed_ProducesSameTrace()
    {
        var first = RunScenario(42);
        var second = RunScenario(42);

        Assert.Equal(first, second);
    }

    private static string RunScenario(int seed)
    {
        var (world, scheduler) = DemoScenario.Create(seed);

        scheduler.Tick(world);
        scheduler.Tick(world);
        scheduler.Tick(world);

        foreach (var agent in world.Agents.Where(a => a.HasDirectKnowledge))
            agent.ResourceLevel = 100;

        scheduler.Tick(world);

        for (var generation = 1; generation <= 3; generation++)
        {
            world.Generation = generation;
            scheduler.Tick(world);
        }

        return DemoScenario.Trace(world);
    }
}
