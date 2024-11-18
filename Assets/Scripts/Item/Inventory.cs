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
}
