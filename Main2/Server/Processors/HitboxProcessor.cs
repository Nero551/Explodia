using System;
using Blocks;
using Godot;

namespace Processors;

public class HitboxProcessor : Processor
{

    StateProcessor stateProcessor;
    HealthProcessor healthProcessor;
    AnimationProcessor animationProcessor;

    public override bool CheckProcessorDependancies()
    {
        return Processor.Has<StateProcessor, AnimationProcessor, HealthProcessor>();
    }

    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.HitboxBlock>();
    }

    public override void Start()
    {
        base.Start();
        EventService.Subscribe<Events.EntityCreation>(OnEntityCreation);
        stateProcessor = Processor.Get<StateProcessor>();
        healthProcessor = Processor.Get<HealthProcessor>();
        animationProcessor = Processor.Get<AnimationProcessor>();
    }

    void OnEntityCreation(Events.EntityCreation evnt)
    {
        if (HasRequiredBlocks(evnt.Entity))
        {
            evnt.Entity.GetNode<Area3D>().BodyEntered += (body) => OnHitboxEntered(evnt.Entity, body);
        }
    }

    void OnHitboxEntered(Entity hitbox, Node3D body)
    {
        var hitboxBlock = hitbox.GetBlock<Blocks.HitboxBlock>();
        Entity attacker = hitboxBlock.Attacker;
        if (body.GetOwner().HasMeta("entity_id"))
        {
            Entity targetHitEntity = Entity.Get((int)body.GetOwner().GetMeta("entity_id"));

            if (targetHitEntity != null && targetHitEntity is Entities.Character targetCharacter && targetHitEntity != attacker)
            {
                if (hitboxBlock.HitTargets.ContainsKey(targetCharacter))
                {
                    // int hits = Data.ContainsKey("Hits") ? (int)Data["Hits"] : 1;
                    int hits = 1;

                    if (hitboxBlock.HitTargets[targetCharacter] >= hits)
                    {
                        return;
                    }
                    else
                    {
                        hitboxBlock.HitTargets[targetCharacter]++;
                    }
                }
                else
                {
                    hitboxBlock.HitTargets.Add(targetCharacter, 1);
                }
                DefaultHit(hitbox, attacker, targetCharacter);
            }
        }
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

    void DefaultHit(Entity hitbox, Entity attacker, Entities.Character targetHit)
    {
        if (stateProcessor.HasState(targetHit, "Invulnerable"))
        {
            return;
        }

        //States
        stateProcessor.AddState(attacker, "In Combat", 30);
        stateProcessor.AddState(targetHit, "In Combat", 30);
        stateProcessor.AddState(targetHit, "Stunned", 0.4);

        //Damage
        healthProcessor.DecreaseHealth(targetHit, 15); //* itemdata damage

        //Knockback
        Vector3 direction = -attacker.GetBlock<Blocks.TransformBlock>().Basis.Z;
        if (attacker.GetBlock<Blocks.AttackBlock>().SwingNumber == 4) //* itemdata swings
        {
            targetHit.GetBlock<Blocks.MovementBlock>().Velocity += direction * 15;
        }
        else
        {
            targetHit.GetBlock<Blocks.MovementBlock>().Velocity += direction * 1.5f;
            attacker.GetBlock<Blocks.MovementBlock>().Velocity += direction * 6;
        }


        //Animations, VFX & Sound
        animationProcessor.PlayAnim(
            targetHit, "HitReactions/" + attacker.GetBlock<Blocks.AttackBlock>().SwingNumber, 1);
        NetworkService.SendToAllClients<RemoteEvents.ClientVFX>(
            "Shared/Assets/VFX/HitImpact/HitImpact", targetHit.Id, true, AttachmentPoint.Root);

        NetworkService.SendToAllClients<RemoteEvents.ClientSound>(
            "Shared/Assets/Audio/SFX/punch.wav", targetHit.Id);
    }
}
