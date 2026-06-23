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
    private readonly static Dictionary<Type, Processor> ProcessorLookup = [];

    public static Processor Add<T>() where T : Processor, new()
    {
        T processor = new();
        ProcessorLookup.Add(typeof(T), processor);
        Game.Runtime.Processors.Add(processor);
        return processor;
    }

    public static T Get<T>() where T : Processor, new()
    {
        return (T)ProcessorLookup[typeof(T)] ?? throw new Exception("Processor Doesn't Exist");
    }

    public static bool Has<T>() where T : Processor
    {
        if (ProcessorLookup.ContainsKey(typeof(T)))
        {
            return true;
        }
        return false;
    }
    public static bool Has<T1, T2>() where T1 : Processor where T2 : Processor
    {
        return Has<T1>() && Has<T2>();
    }
    public static bool Has<T1, T2, T3>() where T1 : Processor where T2 : Processor where T3 : Processor
    {
        return Has<T1>() && Has<T2>() && Has<T3>();
    }

    public virtual bool HasRequiredBlocks(Entity entity)
    {
        return true;
    }

    public virtual bool CheckProcessorDependancies()
    {
        return true;
    }

    public virtual void Start()
    {
        foreach (Entity entity in Game.Runtime.Entities)
        {
            if (HasRequiredBlocks(entity))
            {
                StartEntities(entity);
            }
        }
    }

    public virtual void StartEntities(Entity entity) { }

    public virtual void ProcessEntities(Entity entity, double delta) { }

    public virtual void PhysicsProcessEntities(Entity entity, double delta) { }

    public virtual void Process(double delta)
    {
        foreach (Entity entity in Game.Runtime.Entities)
        {
            if (HasRequiredBlocks(entity))
            {
                ProcessEntities(entity, delta);
            }
        }
    }

    public virtual void PhysicsProcess(double delta)
    {
        foreach (Entity entity in Game.Runtime.Entities)
        {
            if (HasRequiredBlocks(entity))
            {
                PhysicsProcessEntities(entity, delta);
            }
        }
    }

    public virtual void InputProcess(InputEvent inputEvent)
    {
        foreach (Entity entity in Game.Runtime.Entities)
        {
            if (HasRequiredBlocks(entity))
            {
                InputProcessEntities(entity, inputEvent);
            }
        }
    }

    public virtual void InputProcessEntities(Entity entity, InputEvent inputEvent) { }
}