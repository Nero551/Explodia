using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Godot;
using Processors;

namespace RemoteEvents.Replication;

public class Replication : RemoteEvent
{
    private int ArrayLength;
    public List<ReplicationBox> ReplicationQueue = [];

    public override byte[] Encode()
    {
        base.Encode();

        //* Writing Here
        WriteInt(((List<ReplicationBox>)Data[0]).Count);
        foreach (ReplicationBox replicationBox in (List<ReplicationBox>)Data[0])
        {
            WriteInt(replicationBox.EntityId);
            WriteInt(replicationBox.BlockId);
            WriteInt(replicationBox.FieldId);
            bool isEnum = replicationBox.Value.GetType().IsEnum;
            WriteBool(isEnum);
            if (isEnum)
            {
                WriteEnum(replicationBox.Value);
            }
            else
            {
                WriteObject(replicationBox.Value);
            }
        }
        return CreateBytesArray();
    }

    public override void Decode()
    {
        base.Decode();
        //* Reading Here
        ArrayLength = ReadInt();
        for (int i = 0; i < ArrayLength; i++)

        {

            int entityId = ReadInt();
            int blockId = ReadInt();
            int fieldId = ReadInt();
            bool isEnum = ReadBool();
            object value;
            if (isEnum)
            {
                value = ReadByte();
            }
            else
            {
                value = ReadObject();
            }
            ReplicationQueue.Add(new ReplicationBox(entityId, blockId, fieldId, value) { IsEnum = isEnum });
        }
    }
}
