using Godot;
using Godot.NativeInterop;

partial class Attachment3D
{
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void SaveGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.SaveGodotObjectData(info);
        info.AddProperty(PropertyName.@AttachWhat, global::Godot.Variant.From<global::Godot.Node3D>(this.@AttachWhat));
        info.AddProperty(PropertyName.@AttachTo, global::Godot.Variant.From<global::Godot.Node3D>(this.@AttachTo));
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void RestoreGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.RestoreGodotObjectData(info);
        if (info.TryGetProperty(PropertyName.@AttachWhat, out var _value_AttachWhat))
            this.@AttachWhat = _value_AttachWhat.As<global::Godot.Node3D>();
        if (info.TryGetProperty(PropertyName.@AttachTo, out var _value_AttachTo))
            this.@AttachTo = _value_AttachTo.As<global::Godot.Node3D>();
    }
}
