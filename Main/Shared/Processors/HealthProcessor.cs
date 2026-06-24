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
            TimerService.CreateTimer<Entity>(evnt.Entity,healthBlock.RegenRate , true, HealthRegeneration);
        }
    }

    void HealthRegeneration(Entity entity)
    {
		GD.Print("nice");
        var healthBlock = entity.GetBlock<Blocks.HealthBlock>();
        healthBlock.Health = healthBlock.Health > healthBlock.MaxHealth ? healthBlock.MaxHealth : healthBlock.Health;

        if (healthBlock.Health < healthBlock.MaxHealth)
        {
            IncreaseHealth(entity, healthBlock.HealthRegen);
        }
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

