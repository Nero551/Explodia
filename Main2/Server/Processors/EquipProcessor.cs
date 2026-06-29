using System;
using Blocks;
using Godot;

namespace Processors;

public class EquipProcessor : Processor
{
    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.EquippedBlock, Blocks.InventoryBlock>();
    }

    public void Equip(Entity entity, string name)
    {
        if (!HasRequiredBlocks(entity))
            return;

        var equippedBlock = entity.GetBlock<EquippedBlock>();
        var inventoryBlock = entity.GetBlock<InventoryBlock>();

        //TODO- fix this mess please. 5 if checks? you dumb? 
        if (DataService.Find<ItemData>(name, out var itemData))
        {
            if (inventoryBlock.Items.ContainsKey(itemData.Name))
            {
                if (itemData.Type == "Armor")
                {
                    //TODO- figure it out stupid.
                }
                else
                {
                    if (equippedBlock.MainHand == default)
                    {
                        equippedBlock.MainHand = name;
                    }
                    else
                    {
                        if (equippedBlock.OffHand == default)
                        {
                            equippedBlock.OffHand = name;

                        }
                        else
                        {
                            Unequip(entity, equippedBlock.MainHand);
                            equippedBlock.MainHand = name;
                        }
                    }
                }
            }
        }
    }
    //TODO- rethink the framework (might merge processors and services into 1 thing)
    //* if u think about it , processors are just services with an updateLoop , so why are they seperate?

    public void Unequip(Entity entity, string name) { }
}
