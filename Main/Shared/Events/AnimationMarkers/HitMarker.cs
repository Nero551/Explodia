using System;
using Godot;


namespace AnimationMarkers;

public class HitMarker(Entity entity) : Event
{
    public Entity Entity => entity;
}
