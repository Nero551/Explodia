using System;
using Godot;


namespace Blocks;

public class HealthBlock : Block
{
    [Replicated(ReplicationMode.Reliable)] public float Health = 100;
    [Replicated(ReplicationMode.Reliable)] public float MaxHealth = 100;
    [Replicated(ReplicationMode.Reliable)] public float HealthRegen = 1;
    public float RegenRate = 0.5f;
}