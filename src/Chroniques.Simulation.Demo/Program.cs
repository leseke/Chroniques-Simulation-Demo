using Chroniques.Simulation.Demo.Simulation;

var (world, scheduler) = DemoScenario.Create();

Console.WriteLine("Chroniques Simulation Demo");
Console.WriteLine("Deterministic public portfolio scenario\n");

for (var i = 0; i < 3; i++)
{
    scheduler.Tick(world);
    Console.WriteLine(DemoScenario.Trace(world));
}

foreach (var agent in world.Agents.Where(a => a.HasDirectKnowledge))
    agent.ResourceLevel = 100;

scheduler.Tick(world);
Console.WriteLine(DemoScenario.Trace(world));

for (var generation = 1; generation <= 3; generation++)
{
    world.Generation = generation;
    scheduler.Tick(world);
    Console.WriteLine(DemoScenario.Trace(world));
}
