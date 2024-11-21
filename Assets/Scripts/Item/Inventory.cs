using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot[] itemSlots = new ItemSlot[5];

    public ItemData GetItem(ItemType itemType)
    {
        for(int i = 0; i < itemSlots.Length; i++ )
        {
            if (itemSlots[i].itemType == itemType)
            {
                return itemSlots[i].item;
            }
        }
        return null;
    }

    public void EquipItem(ItemData item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].itemType == item.itemType)
            {
                itemSlots[i].SetItem(item);
                break;
            }
        }
    }

    public InventoryData GetInventoryData()
    {
        InventoryData data = new InventoryData(itemSlots.Length);
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot slot = itemSlots[i];
            string itemID = slot.item != null ? slot.item.itemID : null;
            ItemSlotData slotData = new ItemSlotData(slot.itemType, itemID);

            data.itemSlots[i] = slotData;
        }
        return data;
    }

    public void SetInventoryData(InventoryData data)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot slot = itemSlots[i];
            ItemSlotData slotData = data.itemSlots[i];

            if (slot.itemType == slotData.itemType)
            {
                if (!string.IsNullOrEmpty(slotData.itemID))
                {
                    ItemData item = ItemManager.itemManager.GetItemByID(slotData.itemID);
                    slot.SetItem(item);
                }
                else
                {
                    slot.SetItem(null);
                }
            }
        }
    }

}
