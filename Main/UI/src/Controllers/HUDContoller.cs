using System;
using Blocks;
using Godot;

public partial class HUDContoller : Controller
{
    public override void Initialize()
    {
        base.Initialize();
        UI.GetUINode<ProgressBar>("HUD/ProgressBar").Value = Player.Character.GetBlock<HealthBlock>().Health / Player.Character.GetBlock<HealthBlock>().MaxHealth;
    }
}
