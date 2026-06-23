using Blocks;
using Godot;

namespace RemoteEvents;

public class MoveRequest : RemoteEvent
{
    public Vector2 MoveDirection;

    public override int Flag => (int)ENetPacketPeer.FlagUnreliableFragment;

    public override byte[] Encode()
    {
        base.Encode();
        WriteVector2((Vector2)Data[0]);
        return CreateBytesArray();
    }

    public override void Decode()
    {
        base.Decode();
        MoveDirection = ReadVector2();
    }
}
