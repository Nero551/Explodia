using Godot;
using System;

public static class VisualService
{
    public static VisualEffect Spawn(string filepath, Node parent, Vector3? pos = null, float lifeTime = 5f)
    {
        PackedScene scene = GD.Load<PackedScene>($"res://Main/{filepath}.tscn");
        if (scene == null)
        {
            return null;
        }

        VisualEffect vfx = scene.Instantiate<VisualEffect>();
        parent.AddChild(vfx);
        if (pos is Vector3 p)
        {
            vfx.GlobalPosition = p;
        }

        vfx.Emit = true;
        vfx.DestroyAsync(lifeTime);
        return vfx;
    }
}
