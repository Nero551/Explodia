using Godot;
using Godot.NativeInterop;

partial class World
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the properties and fields contained in this class, for fast lookup.
    /// </summary>
    public new class PropertyName : global::Godot.Node.PropertyName {
        /// <summary>
        /// Cached name for the 'Hitboxes' field.
        /// </summary>
        public new static readonly global::Godot.StringName @Hitboxes = "Hitboxes";
        /// <summary>
        /// Cached name for the 'Players' field.
        /// </summary>
        public new static readonly global::Godot.StringName @Players = "Players";
        /// <summary>
        /// Cached name for the 'Characters' field.
        /// </summary>
        public new static readonly global::Godot.StringName @Characters = "Characters";
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool SetGodotClassPropertyValue(in godot_string_name name, in godot_variant value)
    {
        if (name == PropertyName.@Hitboxes) {
            this.@Hitboxes = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Godot.Node>(value);
            return true;
        }
        if (name == PropertyName.@Players) {
            this.@Players = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Godot.Node>(value);
            return true;
        }
        if (name == PropertyName.@Characters) {
            this.@Characters = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Godot.Node>(value);
            return true;
        }
        return base.SetGodotClassPropertyValue(name, value);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool GetGodotClassPropertyValue(in godot_string_name name, out godot_variant value)
    {
        if (name == PropertyName.@Hitboxes) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Godot.Node>(this.@Hitboxes);
            return true;
        }
        if (name == PropertyName.@Players) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Godot.Node>(this.@Players);
            return true;
        }
        if (name == PropertyName.@Characters) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Godot.Node>(this.@Characters);
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
        properties.Add(new(type: (global::Godot.Variant.Type)24, name: PropertyName.@Hitboxes, hint: (global::Godot.PropertyHint)0, hintString: "", usage: (global::Godot.PropertyUsageFlags)4096, exported: false));
        properties.Add(new(type: (global::Godot.Variant.Type)24, name: PropertyName.@Players, hint: (global::Godot.PropertyHint)0, hintString: "", usage: (global::Godot.PropertyUsageFlags)4096, exported: false));
        properties.Add(new(type: (global::Godot.Variant.Type)24, name: PropertyName.@Characters, hint: (global::Godot.PropertyHint)0, hintString: "", usage: (global::Godot.PropertyUsageFlags)4096, exported: false));
        return properties;
    }
#pragma warning restore CS0109
}
