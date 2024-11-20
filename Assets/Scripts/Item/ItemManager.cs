using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager itemManager;
    public Inventory inventory;
    public ItemShop itemShop;
    public UIItemInfo uiItemInfo;
    public UIItemChoice uiItemChoice;

    private ItemData[] itemDatas;
    private Dictionary<string, ItemData> itemDataDictionary;

    private void Awake()
    {
        if(itemManager == null)
        {
            itemManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    // �����ִ� UI�� Script�� ������ �ȵǱ� ������ �ʱ� ������ ���� ó���� ���ش�.
    // �ʱ� ������ ������ ������ UI�� �������� null������ �Ǿ��־� ������ �߻��Ѵ�.
    private void Init()
    {
        uiItemInfo.gameObject.SetActive(true);
        uiItemChoice.gameObject.SetActive(true);


        //������ �����͵� ��������(�����丵 ���..)
        itemDatas = Resources.LoadAll<ItemData>("ScriptableObject");
        itemDataDictionary = new Dictionary<string, ItemData>();

        for (int i = 0; i < itemDatas.Length; i++)
        {
            ItemData itemData = itemDatas[i];
            itemDataDictionary[itemData.itemID] = itemData;
        }
    }

    public ItemData GetItemByID(string itemID)
    {
        if (itemDataDictionary.TryGetValue(itemID, out ItemData itemData))
        {
            return itemData;
        }
        return null;
    }
}
