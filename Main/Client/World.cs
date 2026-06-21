using System;
using System.Diagnostics;
using Godot;

public partial class World : Node
{
    // The World The Client Sees.
    public Node Hitboxes;
    public Node Players;
    public Node Characters;

    public override void _EnterTree()
    {
        Players = GetNodeOrNull<Node>("Players");
        Characters = GetNodeOrNull<Node>("Characters");
        Hitboxes = GetNodeOrNull<Node>("Hitboxes");
    }

    public static void Create()
    {
        Game.World = SceneService.CreateScene<World>("World");
        Game.game.AddChild(Game.World);
    }
}