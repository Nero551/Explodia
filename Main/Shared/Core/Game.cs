using System;
using Godot;

public partial class Game : Node
{
    public static Node game;

    public static World World;
    public static Runtime Runtime;
    /*
        PEBS, Processor Entity Block Service. its ECS but godot style.
        starts here. branches out to ServerRuntime and ClientRuntime. then branches out to processors.
        entities contain blocks. blocks are well. blocks of data
        services are just outside forces that help and arn't really a processor (Networking, Audio).
    */

    //TODO- integrate the game to the new framework and network

    public override void _EnterTree()
    {
        game = this;
        Runtime = NetworkService.IsServer() ? new ServerRuntime() : new ClientRuntime();
        Runtime.Start();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Runtime.Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Runtime.PhysicsProcess(delta);
    }

    public override void _Input(InputEvent inputEvent)
    {
        base._Input(inputEvent);
        Runtime.InputProcess(inputEvent);
    }
}
