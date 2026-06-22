using System;
using System.Diagnostics;
using Godot;

public partial class World : Node
{
    // The World The Client Sees.
    public Node Players;
    public Node Workspace;
    public Node Lighting;
    public Camera Camera;

    public override void _EnterTree()
    {
        Game.World = this;
        Workspace = GetNodeOrNull<Node>("Workspace");
        Players = GetNodeOrNull<Node>("Players");
        Lighting = GetNodeOrNull<Node>("Lighting");
        Camera = GetNodeOrNull<Camera>("Camera");
    }

    public static void Create()
    {
        SceneService.CreateScene<World>("World");
        ((SceneTree)Engine.GetMainLoop()).Root.GetNode<Node>("Game").AddChild(Game.World);
    }
}