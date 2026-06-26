using System;
using System.Diagnostics;
using Godot;

public partial class World : Node
{
    public Node Players;
    public Node Workspace;
    public Node Lighting;
    public Camera Camera;
    public UI UI;

    public override void _EnterTree()
    {
        Game.World = this;
        Workspace = GetNodeOrNull<Node>("Workspace");
        Players = GetNodeOrNull<Node>("Players");
        Lighting = GetNodeOrNull<Node>("Lighting");
        Camera = GetNodeOrNull<Camera>("Camera");
        UI = GetNode<UI>("UI");
    }
}