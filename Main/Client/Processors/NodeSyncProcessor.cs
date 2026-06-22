using System;
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

    public override void PhysicsProcessEntities(Entity entity, double delta)
    {
        base.PhysicsProcessEntities(entity, delta);
        if (entity.HasBlock<Blocks.TransformBlock>())
        {
            var node = entity.GetNode<CharacterBody3D>();
            var target = entity.GetBlock<Blocks.TransformBlock>().Position;
            node.Position = node.Position.Lerp(target, 0.05f);
        }

    }
}

