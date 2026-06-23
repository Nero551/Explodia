using System;
using Godot;

namespace Processors;

public class AnimationsProcessor : Processor
{
    Processors.StateProcessor stateProcessor;

    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.AnimationBlock, Blocks.StateBlock>();
    }

    public override void Start()
    {
        base.Start();
        stateProcessor = Processor.Get<Processors.StateProcessor>();
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
        if (priority <= animationBlock.CurrentAnimationPriority)
        {
            animationBlock.CurrentAnimation = animName;
        }
    }

    void MainAnimations(Entity entity)
    {
        var animationBlock = entity.GetBlock<Blocks.AnimationBlock>();
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();

        if (stateBlock.MainState == MainState.Moving)
        {
            if (stateProcessor.CheckState(entity, "Sprinting"))
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
}

