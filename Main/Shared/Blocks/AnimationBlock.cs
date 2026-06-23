using System;
using Godot;


namespace Blocks;

public class AnimationBlock : Block
{
    [Replicated(ReplicationMode.Reliable)] public string CurrentAnimation = "";

    public string Idle = "Default/Idle";
    public string Run = "Default/Run";
    public string Walk = "Default/Walk";

    public int CurrentPriority = 3;
    public double CurrentLength;
}

