using System;
using System.Linq;
using Godot;

namespace RemoteEvents;

public class ClientVFX : RemoteEvent
{
    public string FilePath;
    public Entity SpawnTarget;
    public AttachmentPoint? AttachmentPoint;
    private bool IsAttachmentPoint;

    public override int Flag => (int)ENetPacketPeer.FlagReliable;

    public override byte[] Encode()
    {
        base.Encode();
        //* Writing Here
        WriteString((string)Data[0]);
        WriteInt((int)Data[1]);
        WriteBool((bool)Data[2]);

        if ((bool)Data[2] == true)
        {
            WriteEnum(Data[3]);
        }
        return CreateBytesArray();
    }

    public override void Decode()
    {
        base.Decode();
        //* Reading Here
        FilePath = ReadString();
        SpawnTarget = Entity.Get(ReadInt());
        IsAttachmentPoint = ReadBool();
        AttachmentPoint = null;
        if (IsAttachmentPoint)
        {
            AttachmentPoint = ReadEnum<AttachmentPoint>();
        }
    }
}


