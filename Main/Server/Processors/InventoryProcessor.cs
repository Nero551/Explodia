using System;
using Events;
using Godot;

namespace Processors;

public class InventoryProcessor : Processor
{
    //TODO- inventory
    //* players will have inventory block which has a dictionary of <int,InventoryEntry>
    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.InventoryBlock>();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Process(double delta)
    {
        base.Process(delta);

    }

    public void AddItem() { }

    public void RemoveItem() { }
}

