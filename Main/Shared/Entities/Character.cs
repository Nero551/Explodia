using System;
using Blocks;
using Godot;

namespace Entities;

public class Character : Entity
{

    protected override void Initialize()
    {
        base.Initialize();
        AddBlock<MovementBlock>();
        AddBlock<TransformBlock>();
        AddBlock<StateBlock>();
        AddBlock<AnimationBlock>();
        Game.World.Workspace.GetNode<Node>("Characters").AddChild(ConnectTo(SceneService.CreateScene<CharacterBody3D>("Character")));

    }
}

