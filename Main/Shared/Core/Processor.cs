using System;
using System.Collections.Generic;
using Godot;

namespace Processors { }

/// <summary>
/// Base class for all simulation processors in the Depths framework.
/// 
/// A Processor is a system that operates globally or over all entities that satisfy
/// a specific block requirement set. It defines lifecycle hooks for:
/// - initialization (Start)
/// - frame processing (Process)
/// - physics processing (PhysicsProcess)
/// </summary>
public abstract class Processor
{
    public static Processor Add<T>() where T : Processor, new()
    {
        T processor = new();
        Game.Runtime.Processors.Add(processor);
        return processor;
    }

    public virtual bool HasRequiredBlocks(Entity entity)
    {
        return true;
    }

    public virtual void Start()
    {
        for (int i = 0; i < Game.Runtime.Entities.Count; i++)
        {
            if (HasRequiredBlocks(Game.Runtime.Entities[i]))
            {
                StartEntities(Game.Runtime.Entities[i]);
            }
        }
    }

    public virtual void StartEntities(Entity entity) { }

    public virtual void ProcessEntities(Entity entity, double delta) { }

    public virtual void PhysicsProcessEntities(Entity entity, double delta) { }

    public virtual void Process(double delta)
    {
        for (int i = 0; i < Game.Runtime.Entities.Count; i++)
        {
            if (HasRequiredBlocks(Game.Runtime.Entities[i]))
            {
                ProcessEntities(Game.Runtime.Entities[i], delta);
            }
        }
    }

    public virtual void PhysicsProcess(double delta)
    {
        for (int i = 0; i < Game.Runtime.Entities.Count; i++)
        {
            if (HasRequiredBlocks(Game.Runtime.Entities[i]))
            {
                PhysicsProcessEntities(Game.Runtime.Entities[i], delta);
            }
        }
    }

    public virtual void InputProcess(InputEvent inputEvent)
    {
        for (int i = 0; i < Game.Runtime.Entities.Count; i++)
        {
            if (HasRequiredBlocks(Game.Runtime.Entities[i]))
            {
                InputProcessEntities(Game.Runtime.Entities[i], inputEvent);
            }
        }
    }

    public virtual void InputProcessEntities(Entity entity, InputEvent inputEvent) { }
}