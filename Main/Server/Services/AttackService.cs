using System;
using Godot;

public static class AttackService
{
    public static bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.AttackBlock, Blocks.StateBlock, Blocks.AnimationBlock>();
    }

    public static void Start()
    {
        EventService.Subscribe<RemoteEvents.PrimaryAttackRequest>(PrimaryAttack);
        EventService.Subscribe<RemoteEvents.SecondaryAttackRequest>(SecondaryAttack);

        EventService.Subscribe<AnimationMarkers.HitMarker>(OnHitMarker);
    }

    public static void PrimaryAttack(RemoteEvents.PrimaryAttackRequest evnt)
    {
        // ActiveHand = MainHand;
        BasicAttack(evnt.Player.Character);
    }
    public static void SecondaryAttack(RemoteEvents.SecondaryAttackRequest evnt)
    {
        // ActiveHand = OffHand;
        BasicAttack(evnt.Player.Character);
    }

    //TODO- hand block and processor for active hand, main hand and off hand

    public static void BasicAttack(Entity entity)
    {
        if (!HasRequiredBlocks(entity))
            return;

        var attackBlock = entity.GetBlock<Blocks.AttackBlock>();

        if (!StateService.HasState(entity, "Attacking", "Stunned"))
        {
            var weaponData = DataService.Get<WeaponData>("Fist");
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
            Animation swingAnim = AnimationService.GetAnim(entity, $"{itemName}/L{attackBlock.SwingNumber}");
            if (swingAnim == null)
            {
                return;
            }

            StateService.AddState(entity, "Attacking", swingAnim.Length);
            AnimationService.PlayAnim(entity, $"{itemName}/L{attackBlock.SwingNumber}", 1);
        }
    }

    static void OnHitMarker(AnimationMarkers.HitMarker evnt)
    {
        //TODO- make hand service so this can know which weapon was used by getting the attacker's active hand
        // var itemData = combatable.ActiveHand.ItemData;
        // string itemName = (string)itemData["Name"];
        string hitboxName = "Fist" + "Basic Attack Hitbox"; //* itemData name
        Vector3 hitboxSize = new(0.75f, 1, 0.75f); //* itemData Data[HitboxSize]
        Entities.Hitbox hitbox = Entity.Create<Entities.Hitbox>();
        CharacterBody3D attackerNode = evnt.Entity.GetNode<CharacterBody3D>();
        HitboxService.SetHitboxName(hitbox, hitboxName);
        HitboxService.SetHitboxSize(hitbox, hitboxSize);
        HitboxService.SetHitboxPosition(hitbox, attackerNode.GetNode<Marker3D>("Armature/HitboxLocation").GlobalPosition);
        HitboxService.SetHitboxAttacker(hitbox, evnt.Entity);
        HitboxService.SetHitboxDuration(hitbox, 0.4f);
    }
}
