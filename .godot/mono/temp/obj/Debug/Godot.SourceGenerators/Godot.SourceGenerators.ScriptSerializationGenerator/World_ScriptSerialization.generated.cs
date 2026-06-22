using Godot;
using Godot.NativeInterop;

partial class World
{
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void SaveGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.SaveGodotObjectData(info);
        info.AddProperty(PropertyName.@Hitboxes, global::Godot.Variant.From<global::Godot.Node>(this.@Hitboxes));
        info.AddProperty(PropertyName.@Players, global::Godot.Variant.From<global::Godot.Node>(this.@Players));
        info.AddProperty(PropertyName.@Characters, global::Godot.Variant.From<global::Godot.Node>(this.@Characters));
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void RestoreGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.RestoreGodotObjectData(info);
        if (info.TryGetProperty(PropertyName.@Hitboxes, out var _value_Hitboxes))
            this.@Hitboxes = _value_Hitboxes.As<global::Godot.Node>();
        if (info.TryGetProperty(PropertyName.@Players, out var _value_Players))
            this.@Players = _value_Players.As<global::Godot.Node>();
        if (info.TryGetProperty(PropertyName.@Characters, out var _value_Characters))
            this.@Characters = _value_Characters.As<global::Godot.Node>();
    }
}
