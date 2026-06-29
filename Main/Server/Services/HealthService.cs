using System;
using Godot;

public static class HealthService
{
    static bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.HealthBlock>();
    }

    public static void Start()
    {
        EventService.Subscribe<Events.EntityCreation>(OnEntityCreation);
    }

    static void OnEntityCreation(Events.EntityCreation evnt)
    {
        if (!HasRequiredBlocks(evnt.Entity))
            return;
            
        var healthBlock = evnt.Entity.GetBlock<Blocks.HealthBlock>();
        TimerService.CreateTimer<Entity>(evnt.Entity, healthBlock.RegenRate, true, HealthRegeneration);
    }

    static void HealthRegeneration(Entity entity)
    {
        var healthBlock = entity.GetBlock<Blocks.HealthBlock>();
        IncreaseHealth(entity, healthBlock.HealthRegen);
    }

    public static void IncreaseHealth(Entity entity, float change)
    {
        var healthBlock = entity.GetBlock<Blocks.HealthBlock>();
        healthBlock.Health = Math.Min(healthBlock.MaxHealth, healthBlock.Health + change);
    }

    public static void DecreaseHealth(Entity entity, float change)
    {
        var healthBlock = entity.GetBlock<Blocks.HealthBlock>();
        healthBlock.Health = Math.Max(0, healthBlock.Health - change);
    }
}
