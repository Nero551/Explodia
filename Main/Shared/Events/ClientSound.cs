using System;
using Godot;

namespace RemoteEvents;

public class ClientSound : RemoteEvent
{
    public string FilePath;
    public Entity SpawnTarget;
    public override int Flag => (int)ENetPacketPeer.FlagReliable;

    public override byte[] Encode()
    {
        base.Encode();
        //* Writing Here
        WriteString((string)Data[0]);
        WriteInt((int)Data[1]);
        return CreateBytesArray();
    }

    public override void Decode()
    {
        base.Decode();
        //* Reading Here
        FilePath = ReadString();
        SpawnTarget = Entity.Get(ReadInt());
    }
}


