using System;
using Godot;

public abstract class Data
{
    public string SourcePath;
    public virtual void Load(Godot.Collections.Dictionary json) { }
}
