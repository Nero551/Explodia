using Godot;
using Godot.NativeInterop;

partial class Character
{
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void SaveGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.SaveGodotObjectData(info);
        info.AddProperty(PropertyName.@ActiveHand, global::Godot.Variant.From<global::Item>(this.@ActiveHand));
        info.AddProperty(PropertyName.@MainHand, global::Godot.Variant.From<global::Item>(this.@MainHand));
        info.AddProperty(PropertyName.@Offhand, global::Godot.Variant.From<global::Item>(this.@Offhand));
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void RestoreGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.RestoreGodotObjectData(info);
        if (info.TryGetProperty(PropertyName.@ActiveHand, out var _value_ActiveHand))
            this.@ActiveHand = _value_ActiveHand.As<global::Item>();
        if (info.TryGetProperty(PropertyName.@MainHand, out var _value_MainHand))
            this.@MainHand = _value_MainHand.As<global::Item>();
        if (info.TryGetProperty(PropertyName.@Offhand, out var _value_Offhand))
            this.@Offhand = _value_Offhand.As<global::Item>();
    }
}
