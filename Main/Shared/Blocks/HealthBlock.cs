using System;
using Godot;


namespace Blocks;

public class HealthBlock : Block
{
    [Replicated(ReplicationMode.Reliable)] public int Health = 100;
    [Replicated(ReplicationMode.Reliable)] public int MaxHealth = 100;
    [Replicated(ReplicationMode.Reliable)] public int HealthRegen = 1;
    public float RegenRate = 0.5f;
}