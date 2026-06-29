using System;
using Blocks;
using Godot;

public static class AIService
{
    static bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.AIBlock>();
    }

    public static void ProcessEntities(Entity entity, double delta)
    {
        if (!HasRequiredBlocks(entity))
            return;

        SearchForTarget(entity);
        Think(entity);
        UpdateState(entity);

    }

    static void SearchForTarget(Entity entity)
    {
        foreach (Entities.Player player in PlayersService.GetPlayers())
        {
            Vector3 pos = entity.GetBlock<Blocks.TransformBlock>().Position;
            Vector3 playerPos = player.Character.GetBlock<Blocks.TransformBlock>().Position;
            if ((playerPos - pos).Length() <= 50)
            {
                entity.GetBlock<AIBlock>().Target = player.Character;
                break;
            }
        }
    }

    static void Follow(Entity entity)
    {
        StateService.AddState(entity, "Sprinting");
        var aiBlock = entity.GetBlock<AIBlock>();
        Vector3 pos = entity.GetBlock<Blocks.TransformBlock>().Position;
        Vector3 direction = (pos - aiBlock.Target.GetBlock<TransformBlock>().Position).Normalized();
        entity.GetBlock<MovementBlock>().MoveDirection = new Vector2(-direction.X, direction.Z);
    }

    static void Attack(Entity entity)
    {
        AttackService.BasicAttack(entity);
    }

    static void Idle(Entity entity)
    {

    }

    static void UpdateState(Entity entity)
    {
        switch (entity.GetBlock<AIBlock>().AIState)
        {
            case AIState.Attack:
                Attack(entity);
                break;

            case AIState.Follow:
                Follow(entity);
                break;

            case AIState.Idle:
                Idle(entity);
                break;
        }
    }

    static void Think(Entity entity)
    {
        var aiBlock = entity.GetBlock<AIBlock>();
        if (aiBlock.Target == null)
        {
            aiBlock.AIState = AIState.Idle;
            return;
        }

        if ((aiBlock.Target.GetBlock<TransformBlock>().Position - entity.GetBlock<TransformBlock>().Position).Length() <= 1f)
        {
            if (!StateService.HasState(entity, "Attacking", "Stunned"))
            {
                aiBlock.AIState = AIState.Attack;
                return;
            }
        }
        aiBlock.AIState = AIState.Follow;
        return;
    }
}
