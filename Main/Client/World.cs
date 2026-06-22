using System;
using System.Diagnostics;
using Godot;

public partial class World : Node
{
    // The World The Client Sees.
    public Node Players;
    public Node Workspace;
    public Node Lighting;
    public SpringArm3D Camera;
    public float MaxSpringLength = 6;
    public float MinSpringLength = 1;
    public float MouseSensitivity = 0.002f;

    public override void _EnterTree()
    {
        Workspace = GetNodeOrNull<Node>("Workspace");
        Players = GetNodeOrNull<Node>("Players");
        Lighting = GetNodeOrNull<Node>("Lighting");
        Camera = GetNodeOrNull<SpringArm3D>("Camera");
    }

    public static void Create()
    {
        Game.World = SceneService.CreateScene<World>("World");
        Game.game.AddChild(Game.World);
    }
}