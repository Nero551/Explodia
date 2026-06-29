using System;
using Godot;

namespace Blocks;

public class EquippedBlock : Block
{
    [Replicated(ReplicationMode.Reliable)] public string MainHand = "";
    [Replicated(ReplicationMode.Reliable)] public string OffHand = "";

    [Replicated(ReplicationMode.Reliable)] public string Head = "";
    [Replicated(ReplicationMode.Reliable)] public string Body = "";
    [Replicated(ReplicationMode.Reliable)] public string Pants = "";
    [Replicated(ReplicationMode.Reliable)] public string Boots = "";

}
