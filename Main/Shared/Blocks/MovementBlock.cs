using System;
using Godot;


namespace Blocks;

public class MovementBlock : Block
{
    [Replicated(ReplicationMode.Unreliable)] public Vector2 MoveDirection;
    [Replicated(ReplicationMode.Reliable)] public float Speed = 2.5f;
    [Replicated(ReplicationMode.Reliable)] public float JumpPower = 5;
    [Replicated(ReplicationMode.Unreliable)] public Vector3 Velocity;
}
