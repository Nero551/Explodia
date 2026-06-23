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
        Game.World.Players.AddChild(ConnectTo(SceneService.CreateScene<Node>("Player")));
    }
}
