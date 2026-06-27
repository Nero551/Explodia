using System;
using Godot;
using Godot.Collections;

public class WeaponData : ItemData
{
    public float Damage;
    public int Swings;
    public float ComboCooldown;
    public float ComboResetTime;
    public Vector3 HitboxSize;

    public override void Load(Dictionary json)
    {
        base.Load(json);
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
