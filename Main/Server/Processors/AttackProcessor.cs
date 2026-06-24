using System;
using Blocks;
using Godot;
using RemoteEvents;

namespace Processors;

public class AttackProcessor : Processor
{
    StateProcessor stateProcessor;
    AnimationProcessor animationProcessor;

    public override bool CheckProcessorDependancies()
    {
        return Processor.Has<StateProcessor, AnimationProcessor>();
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

	//TODO- add animation markers.
	//* am thinking. a modified version of animation player than can store markers and fire events on marker.
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
		GD.Print("nice");
        // var itemData = combatable.ActiveHand.ItemData;
        // string itemName = (string)itemData["Name"];
        // string hitboxName = itemName + "Basic Attack Hitbox";
        // if (Game.World.Workspace.GetNode<Node>("Hitboxes").GetNodeOrNull<Hitbox>(hitboxName) == null)
        // {
        //     PackedScene scene = GD.Load<PackedScene>("res://Main/Workspace/Hitbox.tscn");
        //     Hitbox hitbox = scene.Instantiate<Hitbox>();

        //     hitbox.Name = hitboxName;

        //     var hitboxData = (Godot.Collections.Dictionary)itemData["Hitbox"];
        //     Vector3 hitboxSize = new Vector3((float)hitboxData["X"], (float)hitboxData["Y"], (float)hitboxData["Z"]);

        //     hitbox.Init(ComponentHost.Owner.GetNode<Node3D>("Armature/HitboxLocation").GlobalPosition, hitboxSize, ComponentHost.Owner as Character);
        //     GDHelper.ScheduleRemoval(hitbox, 0.1f);
        // }
    }
}

