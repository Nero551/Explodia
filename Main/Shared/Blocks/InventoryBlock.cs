using System;
using System.Collections.Generic;
using Blocks;
using Godot;

namespace Blocks;

public class InventoryBlock : Block
{
    public Dictionary<string, InventoryEntry> Items = [];
}

