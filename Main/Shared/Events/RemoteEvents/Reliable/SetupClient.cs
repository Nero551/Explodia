using Godot;

namespace RemoteEvents;

public class SetupClient : RemoteEvent
{
    public int UserId;
    public int PeerId;
    public int[] PlayerIds;

    public override int Flag => (int)ENetPacketPeer.FlagReliable;

    public override byte[] Encode()
    {
        base.Encode();

        int peerId = (int)Data[0];
        int userId = (int)Data[1];
        int[] playerIds = (int[])Data[2];
        WriteInt(peerId);
        WriteInt(userId);
        WriteIntArray(playerIds);

        return CreateBytesArray();
    }

    public override void Decode()
    {
        base.Decode();
        PeerId = ReadInt();
        UserId = ReadInt();
        PlayerIds = ReadIntArray();
    }
}
