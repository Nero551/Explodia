using System;
using System.Collections.Generic;
using Godot;

public static class ReplicationService
{
    static readonly Dictionary<ReplicationMode, List<ReplicationBox>> ReplicationQueues = new() {
        {ReplicationMode.Reliable, []},
        {ReplicationMode.Unreliable, []}
    };

    static Timer timer;

    public static void Start()
    {
        timer = TimerService.CreateTimer(0.05f, true, LoopEntities);
        EventService.Subscribe<RemoteEvents.Replication.UnreliableReplication>(OnReplication);
        EventService.Subscribe<RemoteEvents.Replication.ReliableReplication>(OnReplication);
    }


    static void LoopEntities()
    {
        if (!NetworkService.IsServer())
            return;

        foreach (Entity entity in Game.Runtime.Entities)
        {
            LoopBlocks(entity);
        }

        FlushQueues();
    }

    //TODO-  i dont know. timers that work like this

    public static void LoopBlocks(Entity entity)
    {
        foreach (KeyValuePair<int, Blocks.Block> pair in entity.Blocks)
        {
            var block = pair.Value;
            var blockId = pair.Key;
            EnqueueChanges(entity, block, blockId);
        }
    }

    static void FlushQueues()
    {
        var unreliableQueue = ReplicationQueues[ReplicationMode.Unreliable];
        var reliableQueue = ReplicationQueues[ReplicationMode.Reliable];
        if (reliableQueue.Count != 0)
        {
            NetworkService.SendToAllClients<RemoteEvents.Replication.ReliableReplication>(
                reliableQueue);
            reliableQueue.Clear();
        }
        if (unreliableQueue.Count != 0)
        {
            NetworkService.SendToAllClients<RemoteEvents.Replication.UnreliableReplication>(
                unreliableQueue);
            unreliableQueue.Clear();
        }
    }

    static void EnqueueChanges(Entity entity, Blocks.Block block, int blockId)
    {
        var replicatedFields = block.ReplicatedFields;
        var lastReplicatedFields = block.LastReplicatedFields;

        foreach (KeyValuePair<int, ReplicatedField> pair in replicatedFields)
        {
            var replicatedFieldId = pair.Key;
            var replicatedField = pair.Value;
            var value = replicatedField.Field.GetValue(block) ?? throw new Exception($"Value: {replicatedField.Field.Name} is null.");

            if (!lastReplicatedFields.TryGetValue(replicatedFieldId, out var old) || !Equals(value, old))
            {
                lastReplicatedFields[replicatedFieldId] = value;
                ReplicationBox replicationBox = new(entity.Id, blockId, replicatedFieldId, value);
                ReplicationQueues[replicatedField.Attribute.Mode].Add(replicationBox);

                block.Changed.Invoke();
            }
        }
    }

    static void OnReplication(RemoteEvents.Replication.Replication evnt)
    {
        foreach (ReplicationBox replicationBox in evnt.ReplicationQueue)
        {
            ApplyChanges(replicationBox);
        }
    }

    static void ApplyChanges(ReplicationBox replicationBox)
    {
        var entity = Entity.Get(replicationBox.EntityId);
        var block = entity.GetBlock(replicationBox.BlockId);
        var replicatedField = block.ReplicatedFields[replicationBox.FieldId];
        if (replicationBox.IsEnum)
        {
            replicatedField.Field.SetValue(block, Enum.ToObject(replicatedField.Field.FieldType, replicationBox.Value));
        }
        else
        {
            replicatedField.Field.SetValue(block, replicationBox.Value);
        }
        block.Changed.Invoke();
    }
}
