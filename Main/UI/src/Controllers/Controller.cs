using System;
using Godot;

public class Controller
{
    protected Entities.Player Player => Client.Player;
    protected UI UI => Game.World.UI;

    public virtual void Initialize(){

    }
}
