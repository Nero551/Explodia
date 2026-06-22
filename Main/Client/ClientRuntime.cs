using System;
using System.Collections.Generic;
using Godot;

sealed class ClientRuntime : Runtime
{
    protected override void AddProcessors()
    {
        base.AddProcessors();
        Processor.Add<Processors.InputProcessor>();
        Processor.Add<Processors.CameraProcessor>();
    }

    public override void Start()
    {
        World.Create();
        Client.Start();

        base.Start();
    }

    public override void Process(double delta)
    {
        Client.Process(delta);

        base.Process(delta);
    }

    public override void PhysicsProcess(double delta)
    {
        base.PhysicsProcess(delta);
    }
}
