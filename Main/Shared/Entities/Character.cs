using System;
using Godot;

namespace Entities;

public class Character : Entity
{

    protected override void Initialize()
    {
        base.Initialize();

        if (NetworkService.IsClient())
        {
            Game.World.Workspace.GetNode<Node>("Characters").AddChild(ConnectTo(SceneService.CreateScene<CharacterBody3D>("Character")));
        }
    }
}

