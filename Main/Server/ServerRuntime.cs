using System;
using System.Collections.Generic;
using Godot;
using Processors;

sealed class ServerRuntime : Runtime
{
    protected override void AddProcessors()
    {
        base.AddProcessors();
        Processor.Add<MovementProcessor>();
        Processor.Add<AnimationProcessor>();
        Processor.Add<HealthProcessor>();
    }

    public override void Start()
    {
        Server.Start();

        base.Start();
    }

    public override void Process(double delta)
    {
        Server.Process(delta);

        base.Process(delta);
    }

    public override void PhysicsProcess(double delta)
    {
        base.PhysicsProcess(delta);
    }
}
