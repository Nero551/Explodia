using System;
using System.Collections.Generic;
using Godot;
using Processors;

/// <summary>
/// Core runtime manager for the Depths framework.
/// </summary>
/// <remarks>
/// Handles entity registration, processor execution, and global update loops.
/// Runs both server and client-side simulation systems depending on configuration.
/// </remarks>
/// 
public abstract class Runtime
{
    public int NextEntityId = 0;
    public HashSet<Processor> Processors = [];

    public Dictionary<int, Entity> Entities = [];

    protected virtual void AddProcessors()
    {
        Processor.Add<ReplicationProcessor>();
        Processor.Add<NodeSyncProcessor>();
    }

    protected virtual void StartServices()
    {
        NetworkService.Start();
    }

    protected virtual void ProcessServices(double delta)
    {
        TimerService.Process(delta);
    }

    public virtual void Start()
    {
        StartServices();
        AddProcessors();

        foreach (Processor processor in Processors)
        {
            processor.Start();
        }
    }

    public virtual void Process(double delta)
    {
        ProcessServices(delta);
        foreach (Processor processor in Processors)
        {
            processor.Process(delta);
        }
    }

    public virtual void PhysicsProcess(double delta)
    {
        foreach (Processor processor in Processors)
        {
            processor.PhysicsProcess(delta);
        }
    }

    public virtual void InputProcess(InputEvent inputEvent)
    {
        foreach (Processor processor in Processors)
        {
            processor.InputProcess(inputEvent);
        }
    }
}