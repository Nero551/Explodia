using System;
using Blocks;
using Godot;
using RemoteEvents;

namespace Processors;

public class AttackProcessor : Processor
{
    StateProcessor stateProcessor;
    AnimationProcessor animationProcessor;
    HitboxProcessor hitboxProcessor;

    public override bool CheckProcessorDependancies()
    {
        return Processor.Has<StateProcessor, AnimationProcessor, HitboxProcessor>();
    }

    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.AttackBlock, Blocks.StateBlock, Blocks.AnimationBlock>();
    }

    public override void Start()
    {
        base.Start();
        stateProcessor = Processor.Get<StateProcessor>();
        animationProcessor = Processor.Get<AnimationProcessor>();
        hitboxProcessor = Processor.Get<HitboxProcessor>();

        EventService.Subscribe<RemoteEvents.PrimaryAttackRequest>(PrimaryAttack);
        EventService.Subscribe<RemoteEvents.SecondaryAttackRequest>(SecondaryAttack);

        EventService.Subscribe<AnimationMarkers.HitMarker>(OnHitMarker);
    }

    public void PrimaryAttack(RemoteEvents.PrimaryAttackRequest evnt)
    {
        // ActiveHand = MainHand;
        BasicAttack(evnt.Player.Character);
    }
    public void SecondaryAttack(RemoteEvents.SecondaryAttackRequest evnt)
    {
        // ActiveHand = OffHand;
        BasicAttack(evnt.Player.Character);
    }

    //TODO- hand block and processor for active hand, main hand and off hand

    public void BasicAttack(Entity entity)
    {
        var attackBlock = entity.GetBlock<Blocks.AttackBlock>();

        if (!stateProcessor.HasState(entity, "Attacking"))
        {
            // if (combatable.ActiveHand == null || combatable.ActiveHand is not Item || combatable.ActiveHand.AnimationLibrary == null)
            // {
            //     return;
            // }
            // var itemData = combatable.ActiveHand.ItemData;


            if ((PULib.GDHelper.CurrentSTime() - attackBlock.LastComboTime) < 2) //* replace with itemData ComboCooldown Time
            {
                return;
            }
            if ((PULib.GDHelper.CurrentSTime() - attackBlock.LastSwingTime) >= 2) //* Replace with ComboResetTime
            {
                attackBlock.SwingNumber = 0;
            }

            if (attackBlock.SwingNumber >= 4) //* instead of 4, put the swings in the itemData
            {
                attackBlock.LastComboTime = PULib.GDHelper.CurrentSTime();
                attackBlock.SwingNumber = 0;
                return;
            }

            attackBlock.SwingNumber++;
            attackBlock.LastSwingTime = PULib.GDHelper.CurrentSTime();

            string itemName = "Fist"; //*replace with itemData name
            Animation swingAnim = animationProcessor.GetAnim(entity, $"{itemName}/L{attackBlock.SwingNumber}");
            if (swingAnim == null)
            {
                return;
            }

            stateProcessor.AddState(entity, "Attacking", swingAnim.Length);
            animationProcessor.PlayAnim(entity, $"{itemName}/L{attackBlock.SwingNumber}", 1);
        }
    }

    public void OnHitMarker(AnimationMarkers.HitMarker evnt)
    {
        // var itemData = combatable.ActiveHand.ItemData;
        // string itemName = (string)itemData["Name"];
        string hitboxName = "Fist" + "Basic Attack Hitbox"; //* itemData name
        Vector3 hitboxSize = new(0.75f, 1, 0.75f); //* itemData Data[HitboxSize]
        Entities.Hitbox hitbox = Entity.Create<Entities.Hitbox>();
        CharacterBody3D attackerNode = evnt.Entity.GetNode<CharacterBody3D>();
        hitboxProcessor.SetHitboxName(hitbox, hitboxName);
        hitboxProcessor.SetHitboxSize(hitbox, hitboxSize);
        hitboxProcessor.SetHitboxPosition(hitbox, attackerNode.GetNode<Marker3D>("Armature/HitboxLocation").GlobalPosition);
        hitboxProcessor.SetHitboxAttacker(hitbox, evnt.Entity);
		hitboxProcessor.SetHitboxDuration(hitbox, 0.4f);
    }
}

