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
        EventService.Subscribe<Events.EntityCreation>(OnEntityCreation);
    }

    void OnEntityCreation(Events.EntityCreation evnt)
    {
        if (HasRequiredBlocks(evnt.Entity))
        {
            var healthBlock = evnt.Entity.GetBlock<Blocks.HealthBlock>();
            TimerService.CreateTimer<Entity>(evnt.Entity, healthBlock.RegenRate, true, HealthRegeneration);
        }
    }

    void HealthRegeneration(Entity entity)
    {
        var healthBlock = entity.GetBlock<Blocks.HealthBlock>();
        IncreaseHealth(entity, healthBlock.HealthRegen);
    }

    public void IncreaseHealth(Entity entity, float change)
    {
        var healthBlock = entity.GetBlock<Blocks.HealthBlock>();
        healthBlock.Health = Math.Min(healthBlock.MaxHealth, healthBlock.Health + change);
    }

    public void DecreaseHealth(Entity entity, float change)
    {
        var healthBlock = entity.GetBlock<Blocks.HealthBlock>();
        healthBlock.Health = Math.Max(0, healthBlock.Health - change);
    }
}

