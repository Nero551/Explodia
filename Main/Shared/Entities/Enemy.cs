using System;
using Godot;
using Blocks;

namespace Entities;
public class Enemy : Entity
{

    protected override void Initialize()
    {
        base.Initialize();
        AddBlock<MovementBlock>();
        AddBlock<TransformBlock>();
        AddBlock<StateBlock>();
        AddBlock<AnimationBlock>();
        AddBlock<HealthBlock>();
        AddBlock<AttackBlock>();
        AddBlock<AIBlock>();
        var node = SceneService.CreateScene<CharacterBody3D>("Enemy");
        ConnectTo(node);
        Game.World.Workspace.AddChild(node);

    }
}

