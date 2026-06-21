using Godot;
using Godot.NativeInterop;

partial class Item
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the properties and fields contained in this class, for fast lookup.
    /// </summary>
    public new class PropertyName : global::Godot.Node3D.PropertyName {
        /// <summary>
        /// Cached name for the 'ItemData' field.
        /// </summary>
        public new static readonly global::Godot.StringName @ItemData = "ItemData";
        /// <summary>
        /// Cached name for the 'AnimationLibrary' field.
        /// </summary>
        public new static readonly global::Godot.StringName @AnimationLibrary = "AnimationLibrary";
        /// <summary>
        /// Cached name for the 'Master' field.
        /// </summary>
        public new static readonly global::Godot.StringName @Master = "Master";
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool SetGodotClassPropertyValue(in godot_string_name name, in godot_variant value)
    {
        if (name == PropertyName.@ItemData) {
            this.@ItemData = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Godot.Collections.Dictionary>(value);
            return true;
        }
        if (name == PropertyName.@AnimationLibrary) {
            this.@AnimationLibrary = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Godot.AnimationLibrary>(value);
            return true;
        }
        if (name == PropertyName.@Master) {
            this.@Master = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Character>(value);
            return true;
        }
        return base.SetGodotClassPropertyValue(name, value);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool GetGodotClassPropertyValue(in godot_string_name name, out godot_variant value)
    {
        if (name == PropertyName.@ItemData) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Godot.Collections.Dictionary>(this.@ItemData);
            return true;
        }
        if (name == PropertyName.@AnimationLibrary) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Godot.AnimationLibrary>(this.@AnimationLibrary);
            return true;
        }
        if (name == PropertyName.@Master) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Character>(this.@Master);
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
        properties.Add(new(type: (global::Godot.Variant.Type)27, name: PropertyName.@ItemData, hint: (global::Godot.PropertyHint)0, hintString: "", usage: (global::Godot.PropertyUsageFlags)4102, exported: true));
        properties.Add(new(type: (global::Godot.Variant.Type)24, name: PropertyName.@AnimationLibrary, hint: (global::Godot.PropertyHint)17, hintString: "AnimationLibrary", usage: (global::Godot.PropertyUsageFlags)4102, exported: true));
        properties.Add(new(type: (global::Godot.Variant.Type)24, name: PropertyName.@Master, hint: (global::Godot.PropertyHint)34, hintString: "CharacterBody3D", usage: (global::Godot.PropertyUsageFlags)4102, exported: true));
        return properties;
    }
#pragma warning restore CS0109
}
