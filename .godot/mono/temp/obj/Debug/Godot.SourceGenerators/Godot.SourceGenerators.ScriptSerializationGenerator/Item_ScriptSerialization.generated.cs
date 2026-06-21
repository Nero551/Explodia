using Godot;
using Godot.NativeInterop;

partial class Item
{
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void SaveGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.SaveGodotObjectData(info);
        info.AddProperty(PropertyName.@ItemData, global::Godot.Variant.From<global::Godot.Collections.Dictionary>(this.@ItemData));
        info.AddProperty(PropertyName.@AnimationLibrary, global::Godot.Variant.From<global::Godot.AnimationLibrary>(this.@AnimationLibrary));
        info.AddProperty(PropertyName.@Master, global::Godot.Variant.From<global::Character>(this.@Master));
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void RestoreGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.RestoreGodotObjectData(info);
        if (info.TryGetProperty(PropertyName.@ItemData, out var _value_ItemData))
            this.@ItemData = _value_ItemData.As<global::Godot.Collections.Dictionary>();
        if (info.TryGetProperty(PropertyName.@AnimationLibrary, out var _value_AnimationLibrary))
            this.@AnimationLibrary = _value_AnimationLibrary.As<global::Godot.AnimationLibrary>();
        if (info.TryGetProperty(PropertyName.@Master, out var _value_Master))
            this.@Master = _value_Master.As<global::Character>();
    }
}
