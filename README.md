# Chroniques Simulation Demo

A compact, public C#/.NET demonstration of deterministic simulation architecture.

> This repository is an independent portfolio demo. It does **not** contain source code, documentation, assets, or proprietary implementation details from the private Chroniques repositories.

## What this demonstrates

- deterministic world simulation with a seeded state;
- entity/component-style state modeling;
- ordered systems executed by a scheduler;
- progressive information propagation across agents;
- event resolution and memory evolution;
- reproducible replay tests with xUnit;
- clean separation between simulation logic and tests.

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

## Run

```bash
dotnet run --project src/Chroniques.Simulation.Demo
```

## Test

```bash
dotnet test
```

## Why this matters

The goal is not to build a game here. The goal is to show a production-oriented development pattern: deterministic behavior, explicit state transitions, testable systems, reproducible scenarios, and clear hand-off documentation.

## Portfolio context

This demo complements my automation and data portfolio by showing a different capability: designing and validating a structured C#/.NET simulation engine rather than only scripts, spreadsheets, or data utilities.
