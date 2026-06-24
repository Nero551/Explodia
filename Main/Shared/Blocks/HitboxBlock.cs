using System;
using System.Collections.Generic;
using Godot;


namespace Blocks;

public class HitboxBlock : Block
{
    public Entity Attacker;
    public Dictionary<Entities.Character, int> HitTargets = [];
	public float Duration;
}

