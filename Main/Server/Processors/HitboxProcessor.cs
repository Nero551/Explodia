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
        EventService.Subscribe<Events.EntityCreation>(OnEntityCreation);
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
                //TODO- add actual effect (getting hit logic).
                //Actual Hit Logic Here pls
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
        // if (targetHit.cStates.CheckState("Invulnerable"))
        // {
        //     return;
        // }

        // //States
        // attacker.cStates.AddState("In Combat", 30);
        // targetHit.cStates.AddState("In Combat", 30);
        // targetHit.cStates.AddState("Stunned", 0.4);
        // //Damage
        // targetHit.cHealth.CurrentHealth -= (float)itemData["Damage"];

        // //Knockback
        // Vector3 direction = -Attacker.Basis.Z;
        // if (Attacker.cCombat.SwingNumber == (int)itemData["Swings"])
        // {
        //     targetHit.cForce.Knockback(direction * 30);
        // }
        // else
        // {
        //     targetHit.cForce.Knockback(direction * 2);
        //     Attacker.cForce.Pull(-direction * 2);
        // }
        // //Animations, VFX & Sound
        // targetHit.cAnimations.PlayAnim("HitReactions/" + Attacker.cCombat.SwingNumber, 1);
        // VisualEffect.Spawn("Shared/Assets/VFX/HitImpact/HitImpact.tscn", targetHit.cBody.Root);
        // AudioService.PlaySpatialSound("Shared/Assets/Audio/SFX/punch.wav", targetHit);
    }
}

