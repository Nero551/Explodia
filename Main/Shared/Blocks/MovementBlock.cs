using System;
using Godot;


namespace Blocks;

public class MovementBlock : Block
{
    [Replicated(ReplicationMode.Unreliable)] public Vector2 MoveDirection;
    [Replicated(ReplicationMode.Reliable)] public float Speed;
    [Replicated(ReplicationMode.Reliable)] public float JumpPower;
    [Replicated(ReplicationMode.Unreliable)] public Vector3 Velocity;
}
