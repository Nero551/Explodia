using System;
using Blocks;
using Godot;

namespace Processors;

public class HealthProcessor : Processor
{
    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.HealthBlock>();
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
        var healthBlock = entity.GetBlock<Blocks.HealthBlock>();

        healthBlock.Health = healthBlock.Health > healthBlock.MaxHealth ? healthBlock.MaxHealth : healthBlock.Health;

        if (healthBlock.Health < healthBlock.MaxHealth)
        {
            IncreaseHealth(entity, healthBlock.HealthRegen);
        }

		//* a way to have per entity timers.
    }

    public void IncreaseHealth(Entity entity, int change)
    {
        var healthBlock = entity.GetBlock<Blocks.HealthBlock>();
        healthBlock.Health += change;
    }

    public void DecreaseHealth(Entity entity, int change)
    {
        var healthBlock = entity.GetBlock<Blocks.HealthBlock>();
        healthBlock.Health -= change;
    }
}

