using System;
using Godot;


namespace Entities;

public class Player : Entity
{
    public Entities.Character Character;
    public int UserId;
    protected override void Initialize()
    {
        base.Initialize();
        Node node = SceneService.CreateScene<Node>("Player");
        ConnectTo(node);
        Game.World.Players.AddChild(node);
    }
}
