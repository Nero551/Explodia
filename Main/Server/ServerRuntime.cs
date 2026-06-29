using System;
using System.Collections.Generic;
using Godot;

sealed class ServerRuntime : Runtime
{
    //*Global
    public override void Start()
    {
        Server.Start();
        StateService.Start();
        AnimationService.Start();
        MovementService.Start();
        HealthService.Start();
        AttackService.Start();
        HitboxService.Start();

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

    public override void InputProcess(InputEvent inputEvent)
    {
        base.InputProcess(inputEvent);
    }

    //*Entities
    public override void StartEntities(Entity entity)
    {
        base.StartEntities(entity);
    }

    public override void ProcessEntities(Entity entity, double delta)
    {
        base.ProcessEntities(entity, delta);
        StateService.ProcessEntities(entity, delta);
        AnimationService.ProcessEntities(entity, delta);
        AIService.ProcessEntities(entity, delta);
    }

    public override void PhysicsProcessEntities(Entity entity, double delta)
    {
        MovementService.PhysicsProcessEntities(entity, delta);
        base.PhysicsProcessEntities(entity, delta);
    }

    public override void InputProcessEntities(Entity entity, InputEvent inputEvent)
    {
        base.InputProcessEntities(entity, inputEvent);
    }
}
