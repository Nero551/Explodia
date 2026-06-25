using System;
using System.Collections.Generic;
using Godot;
using Processors;

sealed class ClientRuntime : Runtime
{
    protected override void AddProcessors()
    {
        base.AddProcessors();
        Processor.Add<InputProcessor>();
        Processor.Add<CameraProcessor>();
        Processor.Add<ClientEffectsProcessor>();
    }

    public override void Start()
    {
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
