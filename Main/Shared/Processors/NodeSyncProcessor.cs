using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Godot;

namespace Processors;

public class NodeSyncProcessor : Processor
{
    public override bool HasRequiredBlocks(Entity entity)
    {
        return base.HasRequiredBlocks(entity);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Process(double delta)
    {
        base.Process(delta);
    }

    public override void ProcessEntities(Entity entity, double delta)
    {
        base.ProcessEntities(entity, delta);

        //* Animation Sync
        if (entity.HasBlock<Blocks.AnimationBlock>())
        {
            var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
            if (animationPlayer == null)
                return;

            var animationBlock = entity.GetBlock<Blocks.AnimationBlock>();

            if (!animationPlayer.HasAnimation(animationBlock.CurrentAnimation))
                return;

            animationPlayer.Play(animationBlock.CurrentAnimation, 0.5f);
        }
    }

    public override void PhysicsProcessEntities(Entity entity, double delta)
    {
        base.PhysicsProcessEntities(entity, delta);
        if (entity.ConnectedNode == null)
            return;


        //* Transform Sync
        if (entity.HasBlock<Blocks.TransformBlock>())
        {
            var transformBlock = entity.GetBlock<Blocks.TransformBlock>();
            var node = entity.GetNode<CharacterBody3D>();
            var target = transformBlock.Position;
            node.Position = node.Position.Lerp(target, 0.3f);
            node.Basis = node.Basis.Slerp(transformBlock.Basis, 0.2f);
        }
    }
}

