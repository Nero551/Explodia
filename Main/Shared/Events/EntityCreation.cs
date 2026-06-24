using System;
using Godot;


namespace Events;

public class EntityCreation(Entity entity) : Event
{
    public Entity Entity => entity;
}
