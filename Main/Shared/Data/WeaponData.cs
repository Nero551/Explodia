using System;
using Godot;
using Godot.Collections;

public class WeaponData : Data
{
    public string Name;
    public string Type;
    public float Damage;
    public int Swings;
    public float ComboCooldown;
    public float ComboResetTime;
    public Vector3 HitboxSize;

    public override void Load(Dictionary json)
    {
        base.Load(json);
        Name = (string)json["Name"];
        Type = (string)json["Type"];
        Damage = (float)json["Damage"];
        Swings = (int)json["Swings"];
        ComboCooldown = (float)json["ComboCooldown"];
        ComboResetTime = (float)json["ComboResetTime"];
        
        float x = (float)((Godot.Collections.Dictionary)json["Hitbox"])["X"];
        float y = (float)((Godot.Collections.Dictionary)json["Hitbox"])["Y"];
        float z = (float)((Godot.Collections.Dictionary)json["Hitbox"])["Z"];
        HitboxSize = new Vector3(x, y, z);
    }
}
