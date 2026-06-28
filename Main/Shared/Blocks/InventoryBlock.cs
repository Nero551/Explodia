using System;
using System.Collections.Generic;
using Godot;

namespace Blocks;

public class InventoryBlock : Block
{
    public Dictionary<string, InventoryEntry> Items = [];
}

//TODO- make inventory actually work. like equipping and stuff.