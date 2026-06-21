using System;
using Godot;


namespace Entities;

public class Player : Entity
{
    
    protected override void Initialize()
    {

        AddBlock<Blocks.TransformBlock>();
        AddBlock<Blocks.MovementBlock>();
        AddBlock<Blocks.InputBlock>();
        
        if (NetworkService.IsClient())
        {
            Game.World.Players.AddChild(ConnectTo(SceneService.CreateScene<Node>("Player")));
        }
    }
}
