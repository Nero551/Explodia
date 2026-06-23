using System;
using Godot;


namespace Blocks;

public class AnimationBlock : Block
{
    [Replicated(ReplicationMode.Reliable)] public string CurrentAnimation = "Default/Run";
    public int CurrentAnimationPriority = 3;
}

