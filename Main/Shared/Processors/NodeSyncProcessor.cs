using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Godot;

namespace Processors;

public class NodeSyncProcessor : Processor
{
    public override void ProcessEntities(Entity entity, double delta)
    {
        base.ProcessEntities(entity, delta);
        if (entity.ConnectedNode == null)
            return;

        AnimationSync(entity);
    }

    void AnimationSync(Entity entity)
    {
        if (entity.HasBlock<Blocks.AnimationBlock>())
        {
            var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
            if (animationPlayer == null)
                return;

            var animationBlock = entity.GetBlock<Blocks.AnimationBlock>();

            if (!animationPlayer.HasAnimation(animationBlock.CurrentAnimation))
                return;

            if (animationPlayer.CurrentAnimation == animationBlock.CurrentAnimation)
                return;
            GD.Print(animationBlock.CurrentAnimation);

            animationPlayer.Play(animationBlock.CurrentAnimation, 0.4f);
        }
    }

    public override void PhysicsProcessEntities(Entity entity, double delta)
    {
        base.PhysicsProcessEntities(entity, delta);
        if (entity.ConnectedNode == null)
            return;


        TransformSync(entity);
    }

    void TransformSync(Entity entity)
    {
        if (entity.HasBlock<Blocks.TransformBlock>())
        {
            var transformBlock = entity.GetBlock<Blocks.TransformBlock>();
            var node = entity.GetNode<CharacterBody3D>();
            var target = transformBlock.Position;
            node.Position = node.Position.Lerp(target, 0.3f);
            node.Basis = node.Basis.Slerp(transformBlock.Basis, 0.3f);
        }
    }
}

