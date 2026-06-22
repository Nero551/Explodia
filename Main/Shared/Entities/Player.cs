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
        Character = Entity.Create<Entities.Character>();
        // AddBlock<Blocks.TransformBlock>();
        // AddBlock<Blocks.MovementBlock>();

        if (NetworkService.IsClient())
        {
            Character.GetNode<CharacterBody3D>().Name = UserId.ToString();
            Game.World.Players.AddChild(ConnectTo(SceneService.CreateScene<Node>("Player")));
        }
    }
}
