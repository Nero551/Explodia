using System;
using Blocks;
using Godot;
using Processors;

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
        AddBlock<HealthBlock>();
        AddBlock<AttackBlock>();
        var node = SceneService.CreateScene<CharacterBody3D>("Character");
        node.SetMeta("entity_id", Id);
        ConnectTo(node);
        Game.World.Workspace.GetNode<Node>("Characters").AddChild(node);

    }
}

