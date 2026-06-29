using Godot;
using Godot.NativeInterop;

partial class Server
{
partial class ClientInfo
{
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void SaveGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.SaveGodotObjectData(info);
        info.AddProperty(PropertyName.@UserId, global::Godot.Variant.From<int>(this.@UserId));
        info.AddProperty(PropertyName.@PeerId, global::Godot.Variant.From<int>(this.@PeerId));
        info.AddProperty(PropertyName.@Peer, global::Godot.Variant.From<global::Godot.ENetPacketPeer>(this.@Peer));
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void RestoreGodotObjectData(global::Godot.Bridge.GodotSerializationInfo info)
    {
        base.RestoreGodotObjectData(info);
        if (info.TryGetProperty(PropertyName.@UserId, out var _value_UserId))
            this.@UserId = _value_UserId.As<int>();
        if (info.TryGetProperty(PropertyName.@PeerId, out var _value_PeerId))
            this.@PeerId = _value_PeerId.As<int>();
        if (info.TryGetProperty(PropertyName.@Peer, out var _value_Peer))
            this.@Peer = _value_Peer.As<global::Godot.ENetPacketPeer>();
    }
}
}
