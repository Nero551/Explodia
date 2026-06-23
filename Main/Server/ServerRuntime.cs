using System;
using System.Collections.Generic;
using Godot;

sealed class ServerRuntime : Runtime
{
    protected override void AddProcessors()
    {
        base.AddProcessors();
        Processor.Add<Processors.MovementProcessor>();
        Processor.Add<Processors.StateProcessor>();
        Processor.Add<Processors.AnimationProcessor>();

    }

    public override void Start()
    {
        Server.Start();

        //TODO- i need  clear explicit dependancy and communication between processors 
        //* 1- it should be obvious 2- this processor A wont run unless processor B exists.  

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
