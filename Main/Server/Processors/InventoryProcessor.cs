using System;
using Blocks;
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

    public void AddItem(Entity entity, string name, int amount)
    {
        if (!HasRequiredBlocks(entity))
            return;

        var inventoryBlock = entity.GetBlock<InventoryBlock>();

        if (inventoryBlock.Items.ContainsKey(name))
        {
            inventoryBlock.Items[name].Name = name;
            inventoryBlock.Items[name].Amount += amount;
        }
        else
        {
            var entry = new InventoryEntry() { Name = name, Amount = amount };
            inventoryBlock.Items.Add(name, entry);
        }
    }

    public void RemoveItem(Entity entity, string name, int amount)
    {
        if (!HasRequiredBlocks(entity))
            return;

        var inventoryBlock = entity.GetBlock<InventoryBlock>();

        if (inventoryBlock.Items.ContainsKey(name))
        {
            inventoryBlock.Items[name].Name = name;
            inventoryBlock.Items[name].Amount -= amount;

            if (inventoryBlock.Items[name].Amount <= 0)
            {
                inventoryBlock.Items.Remove(name);
            }
        }
        else
        {
            GD.PushWarning($"Item Doesn't Exist in the Player's Inventory: {name}");
            return;
        }
    }
}

