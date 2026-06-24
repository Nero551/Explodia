using System;
using Godot;


namespace Events;

public class Died(Entity entity) : Event
{
    public Entity Entity => entity;
}
