using System;
using Godot;


[GlobalClass]
[Tool]
public partial class ModdedAnimationPlayer : AnimationPlayer
{

    public void OnMarker(string markerName)
    {
        if (markerName == "HitMarker")
        {
            EventService.Fire(new AnimationMarkers.HitMarker());
        }
    }
}


