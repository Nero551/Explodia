using System;
using Godot;

public static class NodeSyncService
{
    public static void ProcessEntities(Entity entity, double delta)
    {
        if (entity.ConnectedNode == null)
            return;

        AnimationSync(entity);
    }

    static void AnimationSync(Entity entity)
    {
        if (entity.HasBlock<Blocks.AnimationBlock>())
        {
            var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<ModdedAnimationPlayer>("ModdedAnimationPlayer");
            if (animationPlayer == null)
                return;

            var animationBlock = entity.GetBlock<Blocks.AnimationBlock>();

            if (!animationPlayer.HasAnimation(animationBlock.CurrentAnimation))
                return;

            if (animationPlayer.CurrentAnimation == animationBlock.CurrentAnimation)
                return;

            animationPlayer.Play(animationBlock.CurrentAnimation, 0.4f);
        }
    }

    public static void PhysicsProcessEntities(Entity entity, double delta)
    {
        if (entity.ConnectedNode == null)
            return;


        TransformSync(entity);
    }

    static void TransformSync(Entity entity)
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
