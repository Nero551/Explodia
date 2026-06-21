using Godot;
using Godot.NativeInterop;

partial class ECharacter
{
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void SaveGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.SaveGodotObjectData(info);
        info.AddProperty(PropertyName.@Rig, global::Godot.Variant.From<global::Godot.Node3D>(this.@Rig));
        info.AddProperty(PropertyName.@ActiveHand, global::Godot.Variant.From<global::EItem>(this.@ActiveHand));
        info.AddProperty(PropertyName.@MainHand, global::Godot.Variant.From<global::EItem>(this.@MainHand));
        info.AddProperty(PropertyName.@Offhand, global::Godot.Variant.From<global::EItem>(this.@Offhand));
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void RestoreGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.RestoreGodotObjectData(info);
        if (info.TryGetProperty(PropertyName.@Rig, out var _value_Rig))
            this.@Rig = _value_Rig.As<global::Godot.Node3D>();
        if (info.TryGetProperty(PropertyName.@ActiveHand, out var _value_ActiveHand))
            this.@ActiveHand = _value_ActiveHand.As<global::EItem>();
        if (info.TryGetProperty(PropertyName.@MainHand, out var _value_MainHand))
            this.@MainHand = _value_MainHand.As<global::EItem>();
        if (info.TryGetProperty(PropertyName.@Offhand, out var _value_Offhand))
            this.@Offhand = _value_Offhand.As<global::EItem>();
    }
}
