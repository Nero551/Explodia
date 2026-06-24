using System;
using System.Collections.Generic;
using Blocks;
using Godot;

namespace Entities;

public class Hitbox : Entity
{
    protected override void Initialize()
    {
        base.Initialize();
		AddBlock<HitboxBlock>();
        Area3D hitbox = SceneService.CreateScene<Area3D>("Hitbox");
        ConnectTo(hitbox);
        Game.World.Workspace.GetNodeOrNull<Node>("Hitboxes").AddChild(hitbox);
    }
}

