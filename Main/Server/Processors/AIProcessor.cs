using System;
using Blocks;
using Godot;

namespace Processors;

public class AIProcessor : Processor
{

    AttackProcessor attackProcessor;
    MovementProcessor movementProcessor;
    StateProcessor stateProcessor;

    public override bool CheckProcessorDependancies()
    {
        return Processor.Has<MovementProcessor, AttackProcessor, StateProcessor>();
    }

    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.AIBlock>();
    }

    public override void Start()
    {
        base.Start();
        attackProcessor = Processor.Get<AttackProcessor>();
        movementProcessor = Processor.Get<MovementProcessor>();
        stateProcessor = Processor.Get<StateProcessor>();
    }

    public override void Process(double delta)
    {
        base.Process(delta);

    }

    public override void ProcessEntities(Entity entity, double delta)
    {
        base.ProcessEntities(entity, delta);
        SearchForTarget(entity);
        Think(entity);
        UpdateState(entity);

    }

    public void SearchForTarget(Entity entity)
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

    public void Follow(Entity entity)
    {
        stateProcessor.AddState(entity, "Sprinting");
        var aiBlock = entity.GetBlock<AIBlock>();
        Vector3 pos = entity.GetBlock<Blocks.TransformBlock>().Position;
        Vector3 direction = (pos - aiBlock.Target.GetBlock<TransformBlock>().Position).Normalized();
        entity.GetBlock<MovementBlock>().MoveDirection = new Vector2(-direction.X, direction.Z);
    }

    public void Attack(Entity entity)
    {
        attackProcessor.BasicAttack(entity);
    }

    public void Idle(Entity entity)
    {

    }

    public void UpdateState(Entity entity)
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

    public void Think(Entity entity)
    {
        var aiBlock = entity.GetBlock<AIBlock>();
        if (aiBlock.Target == null)
        {
            aiBlock.AIState = AIState.Idle;
            return;
        }

        if ((aiBlock.Target.GetBlock<TransformBlock>().Position - entity.GetBlock<TransformBlock>().Position).Length() <= 1f)
        {
            if (!stateProcessor.HasState(entity, "Attacking", "Stunned"))
            {
                aiBlock.AIState = AIState.Attack;
                return;
            }
        }
        aiBlock.AIState = AIState.Follow;
        return;
    }
}

