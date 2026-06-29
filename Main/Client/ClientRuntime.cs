using System;
using System.Collections.Generic;
using Godot;

sealed class ClientRuntime : Runtime
{

    //*Global
    public override void Start()
    {
        Client.Start();
        InputService.Start();
        ClientEffectsService.Start();
        
        base.Start();
    }

    public override void Process(double delta)
    {
        Client.Process(delta);
        InputService.Process(delta);
        CameraService.Process(delta);

        base.Process(delta);
    }

    public override void PhysicsProcess(double delta)
    {
        base.PhysicsProcess(delta);
    }

    public override void InputProcess(InputEvent inputEvent)
    {
        base.InputProcess(inputEvent);
        CameraService.InputProcess(inputEvent);
    }

    //*Entities
    public override void StartEntities(Entity entity)
    {
        base.StartEntities(entity);
    }

    public override void ProcessEntities(Entity entity, double delta)
    {
        base.ProcessEntities(entity, delta);
    }

    public override void PhysicsProcessEntities(Entity entity, double delta)
    {
        base.PhysicsProcessEntities(entity, delta);
    }

    public override void InputProcessEntities(Entity entity, InputEvent inputEvent)
    {
        base.InputProcessEntities(entity, inputEvent);
    }
}
