using System;
using Godot;

public partial class Game : Node
{
    public static World World;
    public static Runtime Runtime;
    /*
        PEBS, Processor Entity Block Service. its ECS but godot style.
        starts here. branches out to ServerRuntime and ClientRuntime. then branches out to processors.
        entities contain blocks. blocks are well. blocks of data
        services are just outside forces that help and arn't really a processor (Networking, Audio).

        keep godot use to a minimum. godot physics and collisions on the server. godot nodes for visuals on the client
    */

    //TODO- integrate the game to the new framework and network

    //TODO- fix bug where processor Start method runs startentities before any entities exist
    //TODO- Start() should run on the start of the program . StartEntities() should run on entity creation
    //* perhaps an event that fires on entity creation. processors can react to that event

    public override void _EnterTree()
    {
        Runtime = NetworkService.IsServer() ? new ServerRuntime() : new ClientRuntime();
    }

    public override async void _Ready()
    {
        base._Ready();
        Runtime.Start();
    }

    public override  void _Process(double delta)
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
