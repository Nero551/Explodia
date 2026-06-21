using Godot;
using Godot.NativeInterop;

partial class Character
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the properties and fields contained in this class, for fast lookup.
    /// </summary>
    public new class PropertyName : global::Godot.CharacterBody3D.PropertyName {
        /// <summary>
        /// Cached name for the 'ActiveHand' property.
        /// </summary>
        public new static readonly global::Godot.StringName @ActiveHand = "ActiveHand";
        /// <summary>
        /// Cached name for the 'MainHand' field.
        /// </summary>
        public new static readonly global::Godot.StringName @MainHand = "MainHand";
        /// <summary>
        /// Cached name for the 'Offhand' field.
        /// </summary>
        public new static readonly global::Godot.StringName @Offhand = "Offhand";
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool SetGodotClassPropertyValue(in godot_string_name name, in godot_variant value)
    {
        if (name == PropertyName.@ActiveHand) {
            this.@ActiveHand = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Item>(value);
            return true;
        }
        if (name == PropertyName.@MainHand) {
            this.@MainHand = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Item>(value);
            return true;
        }
        if (name == PropertyName.@Offhand) {
            this.@Offhand = global::Godot.NativeInterop.VariantUtils.ConvertTo<global::Item>(value);
            return true;
        }
        return base.SetGodotClassPropertyValue(name, value);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool GetGodotClassPropertyValue(in godot_string_name name, out godot_variant value)
    {
        if (name == PropertyName.@ActiveHand) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Item>(this.@ActiveHand);
            return true;
        }
        if (name == PropertyName.@MainHand) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Item>(this.@MainHand);
            return true;
        }
        if (name == PropertyName.@Offhand) {
            value = global::Godot.NativeInterop.VariantUtils.CreateFrom<global::Item>(this.@Offhand);
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
        properties.Add(new(type: (global::Godot.Variant.Type)24, name: PropertyName.@MainHand, hint: (global::Godot.PropertyHint)34, hintString: "Node3D", usage: (global::Godot.PropertyUsageFlags)4102, exported: true));
        properties.Add(new(type: (global::Godot.Variant.Type)24, name: PropertyName.@Offhand, hint: (global::Godot.PropertyHint)34, hintString: "Node3D", usage: (global::Godot.PropertyUsageFlags)4102, exported: true));
        properties.Add(new(type: (global::Godot.Variant.Type)24, name: PropertyName.@ActiveHand, hint: (global::Godot.PropertyHint)34, hintString: "Node3D", usage: (global::Godot.PropertyUsageFlags)4102, exported: true));
        return properties;
    }
#pragma warning restore CS0109
}
