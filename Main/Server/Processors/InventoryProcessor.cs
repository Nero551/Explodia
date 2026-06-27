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

        if (DataService.Find<ItemData>(name, out var itemData))
        {
            if (inventoryBlock.Items.ContainsKey(itemData.Name))
            {
                inventoryBlock.Items[itemData.Name].Name = itemData.Name;
                inventoryBlock.Items[itemData.Name].Amount += amount;
            }
            else
            {
                var entry = new InventoryEntry() { Name = itemData.Name, Amount = amount };
                inventoryBlock.Items.Add(itemData.Name, entry);
            }
        }
    }

    public void RemoveItem(Entity entity, string name, int amount)
    {
        if (!HasRequiredBlocks(entity))
            return;

        var inventoryBlock = entity.GetBlock<InventoryBlock>();

        if (DataService.Find<ItemData>(name, out var itemData))
        {
            if (inventoryBlock.Items.ContainsKey(itemData.Name))
            {
                inventoryBlock.Items[itemData.Name].Name = itemData.Name;
                inventoryBlock.Items[itemData.Name].Amount -= amount;

                if (inventoryBlock.Items[itemData.Name].Amount <= 0)
                {
                    inventoryBlock.Items.Remove(itemData.Name);
                }
            }
            else
            {
                GD.PushWarning($"Item Doesn't Exist in the Player's Inventory: {itemData.Name}");
                return;
            }
        }
    }
}

