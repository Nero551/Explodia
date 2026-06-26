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

        if (!stateProcessor.HasState(entity, "Attacking", "Stunned"))
        {
            var weaponData = DataService.Load<WeaponData>("WeaponData/Fist");
            // if (combatable.ActiveHand == null || combatable.ActiveHand is not Item || combatable.ActiveHand.AnimationLibrary == null)
            // {
            //     return;
            // }
            // var itemData = combatable.ActiveHand.ItemData;


            if ((PULib.GDHelper.CurrentSTime() - attackBlock.LastComboTime) < weaponData.ComboCooldown)
            {
                return;
            }
            if ((PULib.GDHelper.CurrentSTime() - attackBlock.LastSwingTime) >= weaponData.ComboResetTime)
            {
                attackBlock.SwingNumber = 0;
            }

            if (attackBlock.SwingNumber >= weaponData.Swings)
            {
                attackBlock.LastComboTime = PULib.GDHelper.CurrentSTime();
                attackBlock.SwingNumber = 0;
                return;
            }

            attackBlock.SwingNumber++;
            attackBlock.LastSwingTime = PULib.GDHelper.CurrentSTime();

            string itemName = weaponData.Name;
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
        //TODO- make hand service so this can know which weapon was used by getting the attacker's active hand
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

