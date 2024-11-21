using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    private ItemData[] itemDatas;
    private ItemData newItem;

    //private void Start()
    //{
    //    List<ItemData> allItemDatas = new List<ItemData>();

    //    string[] subFolders = { "ScriptableObject/Armor", "ScriptableObject/Boots", "ScriptableObject/Weapon", "ScriptableObject/helmet", "ScriptableObject/Gloves" };
    //    foreach (string folder in subFolders)
    //    {
    //        ItemData[] items = ResourceManager.Instance.LoadAllResources<ItemData>(folder);
    //        if (items != null && items.Length > 0)
    //        {
    //            allItemDatas.AddRange(items);
    //        }
    //    }

    //    itemDatas = allItemDatas.ToArray();

    //    if (itemDatas.Length == 0)
    //    {
    //        return;
    //    }

    //    for (int i = 0; i < itemDatas.Length; i++)
    //    {
    //        Debug.Log(itemDatas[i].name);
    //    }
    //}

    private void Start()
    {
        itemDatas = ResourceManager.Instance.LoadAllResources<ItemData>();

        for (int i = 0; i < itemDatas.Length; i++)
        {
            Debug.Log(itemDatas[i].name);
        }
    }

    public ItemData GetItem()
    {
        int index = Random.Range(0, itemDatas.Length);
        return itemDatas[index];
    }

    public void LetsGatcha()
    {
        newItem = GetItem();
        ItemManager.itemManager.uiItemChoice.ChooseItem(newItem);
    }
}
