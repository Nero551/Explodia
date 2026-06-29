using System;
using System.Collections.Generic;
using Godot;
using Processors;

public abstract class Runtime
{
    public HashSet<Entity> Entities = [];
    public int NextEntityId = 0;

    //*Global
    public virtual void Start()
    {
        NetworkService.Start();
        DataService.Start();

        foreach (Entity entity in Game.Runtime.Entities)
        {
            StartEntities(entity);
        }
        // Entity.Create<Entities.Enemy>();
    }

    public virtual void Process(double delta)
    {
        TimerService.Process(delta);

        foreach (Entity entity in Game.Runtime.Entities)
        {
            ProcessEntities(entity, delta);
        }
    }

    public virtual void PhysicsProcess(double delta)
    {


        foreach (Entity entity in Game.Runtime.Entities)
        {
            PhysicsProcessEntities(entity, delta);
        }
    }

    public virtual void InputProcess(InputEvent inputEvent)
    {


        foreach (Entity entity in Game.Runtime.Entities)
        {
            InputProcessEntities(entity, inputEvent);
        }
    }

    //* Entities
    public virtual void StartEntities(Entity entity)
    {

    }

    public virtual void ProcessEntities(Entity entity, double delta)
    {

    }

    public virtual void PhysicsProcessEntities(Entity entity, double delta)
    {

    }

    public virtual void InputProcessEntities(Entity entity, InputEvent inputEvent)
    {

    }
}