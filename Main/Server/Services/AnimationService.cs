using System;
using Godot;

public static class AnimationService
{
    static bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.AnimationBlock, Blocks.StateBlock>();
    }

    public static void Start()
    {
        EventService.Subscribe<Events.EntityCreation>(OnEntityCreation);
    }


    static void OnEntityCreation(Events.EntityCreation evnt)
    {
        if (!HasRequiredBlocks(evnt.Entity))
            return;

        var animationPlayer = evnt.Entity.ConnectedNode?.GetNodeOrNull<ModdedAnimationPlayer>("ModdedAnimationPlayer");
        animationPlayer.AnimationFinished += animName => OnAnimFinished(evnt.Entity, animName);
    }

    public static void ProcessEntities(Entity entity, double delta)
    {
        if (!HasRequiredBlocks(entity))
            return;
        MainAnimations(entity);
    }

    public static void PlayAnim(Entity entity, string animName, int priority)
    {
        var animationBlock = entity.GetBlock<Blocks.AnimationBlock>();
        if (priority <= animationBlock.CurrentPriority)
        {
            animationBlock.CurrentAnimation = animName;
            animationBlock.CurrentPriority = priority;

            var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<ModdedAnimationPlayer>("ModdedAnimationPlayer");

            if (animationPlayer == null)
                return;

            if (!animationPlayer.HasAnimation(animationBlock.CurrentAnimation))
                return;

            animationBlock.CurrentLength = animationPlayer.GetAnimation(animationBlock.CurrentAnimation).Length;
        }
    }

    public static void LoadAnimLib(Entity entity, string filepath, string animLibName)
    {
        var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<ModdedAnimationPlayer>("ModdedAnimationPlayer");
        var animLib = GD.Load<AnimationLibrary>("res://" + filepath + ".tres");

        if (GetAnimLibrary(entity, animLibName) == null)
        {
            animationPlayer.AddAnimationLibrary(animLibName, animLib);
        }
    }

    public static AnimationLibrary GetAnimLibrary(Entity entity, string libraryName)
    {
        var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<ModdedAnimationPlayer>("ModdedAnimationPlayer");
        if (animationPlayer.HasAnimationLibrary(libraryName))
        {
            return animationPlayer.GetAnimationLibrary(libraryName);
        }
        return null;
    }

    public static Animation GetAnim(Entity entity, string animName)
    {
        var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<ModdedAnimationPlayer>("ModdedAnimationPlayer");
        if (animationPlayer.HasAnimation(animName))
        {
            return animationPlayer.GetAnimation(animName);
        }
        GD.PushWarning("Animation: " + animName + " Doesn't Exist.");
        return null;
    }

    static void MainAnimations(Entity entity)
    {
        var animationBlock = entity.GetBlock<Blocks.AnimationBlock>();
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();

        if (stateBlock.MainState == MainState.Moving)
        {
            if (StateService.HasState(entity, "Sprinting"))
            {
                PlayAnim(entity, animationBlock.Run, 3);
            }
            else
            {
                PlayAnim(entity, animationBlock.Walk, 3);
            }
        }
        else if (stateBlock.MainState == MainState.Idle)
        {
            PlayAnim(entity, animationBlock.Idle, 3);
        }
    }

    static void OnAnimFinished(Entity entity, StringName animName)
    {
        var animationBlock = entity.GetBlock<Blocks.AnimationBlock>();
        animationBlock.CurrentAnimation = "";
        animationBlock.CurrentPriority = 3;
    }
}
