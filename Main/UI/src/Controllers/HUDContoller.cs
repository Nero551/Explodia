using System;
using Blocks;
using Godot;

public partial class HUDContoller : Controller
{

    //TODO- client sided HasState() methods
    //TODO- shared actionVerifier
    //TODO- a way to detect change in fields (needed for UI, EX: Health.Changed(do UI stuf) )
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

    /*

    TODO- Build a change detector for blocks so systems like the UI can subscribe to block changes instead of polling.
    * Add an event to the base Block class.
    * Give it its own subscribe/unsubscribe system.
    * Fire the event whenever a field changes.
    * The best place to fire it is probably in the replication system,
    * since it already loops through blocks every frame and detects changed fields.
    */
}
