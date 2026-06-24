using System;
using Godot;

namespace Processors;

public class AnimationProcessor : Processor
{
    Processors.StateProcessor stateProcessor;

    public override bool CheckProcessorDependancies()
    {
        return Processor.Has<Processors.StateProcessor>();
    }

    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.AnimationBlock, Blocks.StateBlock>();
    }

    public override void Start()
    {
        base.Start();
        stateProcessor = Processor.Get<Processors.StateProcessor>();
        EventService.Subscribe<Events.EntityCreation>(OnEntityCreation);
    }


    void OnEntityCreation(Events.EntityCreation evnt)
    {
        if (HasRequiredBlocks(evnt.Entity))
        {
            var animationPlayer = evnt.Entity.ConnectedNode?.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
            animationPlayer.AnimationFinished += animName => OnAnimFinished(evnt.Entity, animName);
        }
    }

    public override void Process(double delta)
    {
        base.Process(delta);
    }

    public override void ProcessEntities(Entity entity, double delta)
    {
        base.ProcessEntities(entity, delta);
        MainAnimations(entity);
    }

    public void PlayAnim(Entity entity, string animName, int priority)
    {
        var animationBlock = entity.GetBlock<Blocks.AnimationBlock>();
        if (priority <= animationBlock.CurrentPriority)
        {
            animationBlock.CurrentAnimation = animName;
            animationBlock.CurrentPriority = priority;

            var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");

            if (animationPlayer == null)
                return;

            if (!animationPlayer.HasAnimation(animationBlock.CurrentAnimation))
                return;

            animationBlock.CurrentLength = animationPlayer.GetAnimation(animationBlock.CurrentAnimation).Length;
        }
    }

    public void LoadAnimLib(Entity entity, string filepath, string animLibName)
    {
        var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        var animLib = GD.Load<AnimationLibrary>("res://" + filepath + ".tres");

        if (GetAnimLibrary(entity, animLibName) == null)
        {
            animationPlayer.AddAnimationLibrary(animLibName, animLib);
        }
    }

    public AnimationLibrary GetAnimLibrary(Entity entity, string libraryName)
    {
        var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        if (animationPlayer.HasAnimationLibrary(libraryName))
        {
            return animationPlayer.GetAnimationLibrary(libraryName);
        }
        return null;
    }

    public Animation GetAnim(Entity entity, string animName)
    {
        var animationPlayer = entity.ConnectedNode?.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        if (animationPlayer.HasAnimation(animName))
        {
            return animationPlayer.GetAnimation(animName);
        }
        GD.PushWarning("Animation: " + animName + " Doesn't Exist.");
        return null;
    }

    void MainAnimations(Entity entity)
    {
        var animationBlock = entity.GetBlock<Blocks.AnimationBlock>();
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();

        if (stateBlock.MainState == MainState.Moving)
        {
            if (stateProcessor.HasState(entity, "Sprinting"))
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

    void OnAnimFinished(Entity entity, StringName animName)
    {
        var animationBlock = entity.GetBlock<Blocks.AnimationBlock>();
        animationBlock.CurrentAnimation = "";
        animationBlock.CurrentPriority = 3;
    }
}

