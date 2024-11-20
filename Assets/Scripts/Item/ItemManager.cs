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

    // 꺼져있는 UI는 Script가 실행이 안되기 때문에 초기 세팅을 위해 처음에 켜준다.
    // 초기 세팅을 해주지 않으면 UI의 변수들이 null값으로 되어있어 문제가 발생한다.
    private void Init()
    {
        uiItemInfo.gameObject.SetActive(true);
        uiItemChoice.gameObject.SetActive(true);
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
