# Chroniques Simulation Demo

**Deterministic Simulation Engine · C# / .NET 8 · xUnit**

A compact public demonstration of a deterministic simulation architecture built to showcase clean state modeling, ordered systems, reproducible scenarios and automated validation.

> **Portfolio demo:** this repository is intentionally independent. It does **not** contain source code, documentation, assets, or proprietary implementation details from the private Chroniques repositories.

## At a glance

| Capability | Demonstrated here |
| --- | --- |
| Deterministic simulation | Seeded state and reproducible execution |
| Simulation architecture | World state + ordered systems + scheduler |
| Information propagation | Knowledge spreads progressively across agents |
| Event lifecycle | Creation, propagation, resolution and memory evolution |
| Replayability | Same seeded scenario → same logical trace |
| Automated validation | xUnit tests covering core deterministic behavior |

## Architecture

```text
WORLD
  ↓
SCHEDULER
  ↓
ORDERED SYSTEMS
  ├─ event state
  ├─ propagation
  ├─ resolution
  └─ memory evolution
  ↓
DETERMINISTIC RESULT
  ↓
REPLAY / TEST
```

The important point is not the size of the demo: each transition is explicit and testable. Simulation behavior is separated from validation code so scenarios can be reproduced instead of debugged through hidden state.

## Demo scenario

Three agents belong to one small community.

1. Two agents start in a critical resource state.
2. A community event is created.
3. Direct knowledge is assigned to affected agents.
4. A rumor propagates progressively, one new holder per tick.
5. When conditions recover, the event resolves.
6. A world memory is created and can evolve through later generations.
7. Replaying the same seeded scenario produces the same logical trace.

## Project structure

```text
Chroniques.Simulation.Demo/
├─ src/
│  └─ Chroniques.Simulation.Demo/
│     ├─ Chroniques.Simulation.Demo.csproj
│     ├─ Program.cs
│     ├─ Core/
│     └─ Simulation/
└─ tests/
   └─ Chroniques.Simulation.Demo.Tests/
      ├─ Chroniques.Simulation.Demo.Tests.csproj
      └─ DeterministicSimulationTests.cs
```

## Run it

Requirements: **.NET 8 SDK**.

```bash
git clone https://github.com/leseke/Chroniques-Simulation-Demo.git
cd Chroniques-Simulation-Demo
dotnet restore
dotnet build
dotnet test
dotnet run --project src/Chroniques.Simulation.Demo
```

## Engineering focus

This project demonstrates the kind of engineering decisions that matter when a system grows beyond a simple script:

- explicit state transitions;
- deterministic behavior;
- modular simulation systems;
- reproducible scenarios;
- automated regression tests;
- clear separation of responsibilities;
- maintainable hand-off documentation.

## Portfolio context

I work on automation, data-processing utilities and C#/.NET software. This demo complements projects such as **CleanFlow** by showing a different capability: designing and validating a structured simulation engine rather than only scripts, spreadsheets or data utilities.

### Relevant skills

`C#` · `.NET 8` · `xUnit` · `Simulation Architecture` · `Deterministic Systems` · `Automated Testing` · `Software Design`

---

**Need a deterministic workflow, business tool, data utility or C#/.NET component?** This repository is designed as a compact example of how I structure, test and document software before delivery.
