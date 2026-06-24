using System;
using Godot;

namespace Processors;

public class HitboxProcessor : Processor
{
    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.HitboxBlock>();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Process(double delta)
    {
        base.Process(delta);
    }

    public void SetHitboxName(Entity entity, string hitboxName)
    {
        entity.GetNode<Area3D>().Name = hitboxName;
    }

    public void SetHitboxSize(Entity entity, Vector3 hitboxSize)
    {
        entity.GetNode<Area3D>().Scale = hitboxSize;
    }

    public void SetHitboxPosition(Entity entity, Vector3 hitboxPosition)
    {
        entity.GetNode<Area3D>().Position = hitboxPosition;
    }

    public void SetHitboxAttacker(Entity hitbox, Entity attacker)
    {
        hitbox.GetBlock<Blocks.HitboxBlock>().Attacker = attacker;
    }

    public void SetHitboxDuration(Entity hitbox, float duration)
    {
        hitbox.GetBlock<Blocks.HitboxBlock>().Duration = duration;
        TimerService.CreateTimer(hitbox, duration, false, (hitbox) => hitbox.Destroy());
    }
}

