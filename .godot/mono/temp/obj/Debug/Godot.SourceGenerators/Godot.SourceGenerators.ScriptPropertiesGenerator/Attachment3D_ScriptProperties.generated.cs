using Godot;
using Godot.NativeInterop;

partial class Attachment3D
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the properties and fields contained in this class, for fast lookup.
    /// </summary>
    public new class PropertyName : global::Godot.Marker3D.PropertyName {
        /// <summary>
        /// Cached name for the 'AttachWhat' field.
        /// </summary>
        public new static readonly global::Godot.StringName @AttachWhat = "AttachWhat";
        /// <summary>
        /// Cached name for the 'AttachTo' field.
        /// </summary>
        public new static readonly global::Godot.StringName @AttachTo = "AttachTo";
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool SetGodotClassPropertyValue(in godot_string_name name, in godot_variant value)
    {
        if (name == PropertyName.@AttachWhat) {
            this.@AttachWhat = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Godot.Node3D>(value);
            return true;
        }
        if (name == PropertyName.@AttachTo) {
            this.@AttachTo = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Godot.Node3D>(value);
            return true;
        }
        return base.SetGodotClassPropertyValue(name, value);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool GetGodotClassPropertyValue(in godot_string_name name, out godot_variant value)
    {
        if (name == PropertyName.@AttachWhat) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Godot.Node3D>(this.@AttachWhat);
            return true;
        }
        if (name == PropertyName.@AttachTo) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Godot.Node3D>(this.@AttachTo);
            return true;
        }
        return base.GetGodotClassPropertyValue(name, out value);
    }
    /// <summary>
    /// Get the property information for all the properties declared in this class.
    /// This method is used by Godot to register the available properties in the editor.
    /// Do not call this method.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal new static global::System.Collections.Generic.List<global::Godot.Bridge.PropertyInfo> GetGodotPropertyList()
    {
        var properties = new global::System.Collections.Generic.List<global::Godot.Bridge.PropertyInfo>();
        properties.Add(new(type: (global::Godot.Variant.Type)24, name: PropertyName.@AttachWhat, hint: (global::Godot.PropertyHint)0, hintString: "", usage: (global::Godot.PropertyUsageFlags)4102, exported: true));
        properties.Add(new(type: (global::Godot.Variant.Type)24, name: PropertyName.@AttachTo, hint: (global::Godot.PropertyHint)0, hintString: "", usage: (global::Godot.PropertyUsageFlags)4102, exported: true));
        return properties;
    }
#pragma warning restore CS0109
}
