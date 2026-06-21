partial class Item
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
#if TOOLS
    /// <summary>
    /// Get the default values for all properties declared in this class.
    /// This method is used by Godot to determine the value that will be
    /// used by the inspector when resetting properties.
    /// Do not call this method.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal new static global::System.Collections.Generic.Dictionary<global::Godot.StringName, global::Godot.Variant> GetGodotPropertyDefaultValues()
    {
        var values = new global::System.Collections.Generic.Dictionary<global::Godot.StringName, global::Godot.Variant>(3);
        global::Godot.Collections.Dictionary __ItemData_default_value = default;
        values.Add(PropertyName.@ItemData, global::Godot.Variant.From<global::Godot.Collections.Dictionary>(__ItemData_default_value));
        global::Godot.AnimationLibrary __AnimationLibrary_default_value = default;
        values.Add(PropertyName.@AnimationLibrary, global::Godot.Variant.From<global::Godot.AnimationLibrary>(__AnimationLibrary_default_value));
        global::Character __Master_default_value = default;
        values.Add(PropertyName.@Master, global::Godot.Variant.From<global::Character>(__Master_default_value));
        return values;
    }
#endif // TOOLS
#pragma warning restore CS0109
}
