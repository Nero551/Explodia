using System;
using Godot;
using Godot.Collections;

public class ItemData : Data
{
    public string Name;
    public string Type;

    public override void Load(Dictionary json)
    {
        base.Load(json);
        Name = (string)json["Name"];
        Type = (string)json["Type"];
    }
}
