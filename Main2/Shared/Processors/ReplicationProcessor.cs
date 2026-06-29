using System;
using System.Collections;
using System.Collections.Generic;
using Godot;
using RemoteEvents.Replication;

namespace Processors;

public class ReplicationProcessor : Processor
{
    readonly Dictionary<ReplicationMode, List<ReplicationBox>> ReplicationQueues = new() {
        {ReplicationMode.Reliable, []},
        {ReplicationMode.Unreliable, []}
    };

    public override void Start()
    {
        EventService.Subscribe<RemoteEvents.Replication.UnreliableReplication>(OnReplication);
        EventService.Subscribe<RemoteEvents.Replication.ReliableReplication>(OnReplication);
        base.Start();
    }

    double elapsed = 0;
    public override void Process(double delta)
    {
        if (!NetworkService.IsServer())
            return;

        elapsed += delta;
        if (elapsed < 0.05)
            return;

        elapsed = 0;
        base.Process(delta);

        FlushQueues();
    }

    public override void ProcessEntities(Entity entity, double delta)
    {
        base.ProcessEntities(entity, delta);

        foreach (KeyValuePair<int, Blocks.Block> pair in entity.Blocks)
        {
            var block = pair.Value;
            var blockId = pair.Key;
            EnqueueChanges(entity, block, blockId);
        }
    }

    void FlushQueues()
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

    void EnqueueChanges(Entity entity, Blocks.Block block, int blockId)
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

    void OnReplication(RemoteEvents.Replication.Replication evnt)
    {
        foreach (ReplicationBox replicationBox in evnt.ReplicationQueue)
        {
            ApplyChanges(replicationBox);
        }
    }

    void ApplyChanges(ReplicationBox replicationBox)
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
