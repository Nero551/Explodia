using System;
using Blocks;
using Godot;

public partial class HUDContoller : Controller
{

    //TODO- client sided HasState() methods
    //TODO- shared actionVerifier
    public override void Initialize()
    {
        base.Initialize();
        Player.Character.GetBlock<HealthBlock>().Changed += OnHealthChanged;
    }

    void OnHealthChanged()
    {
        UI.GetUINode<ProgressBar>("HUD/ProgressBar").Value =
            Player.Character.GetBlock<HealthBlock>().Health / Player.Character.GetBlock<HealthBlock>().MaxHealth;
    }
}
