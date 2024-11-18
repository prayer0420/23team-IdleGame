using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemChoice : MonoBehaviour
{
    private UIItemInfo equippedItemInfo;
    private UIItemInfo newItemInfo;
    private ItemData equippedItem;
    private ItemData newItem;

    private void Awake()
    {
        equippedItemInfo = transform.Find("EquippedItemInfo").GetComponent<UIItemInfo>();
        newItemInfo = transform.Find("NewItemInfo").GetComponent<UIItemInfo>();

        gameObject.SetActive(false);
    }

    public void ChooseItem(ItemData newItem)
    {
        this.newItem = newItem;
        equippedItem = ItemManager.itemManager.inventory.GetItem(newItem.itemType);
        if (equippedItem == null)
        {
            ItemManager.itemManager.inventory.EquipItem(newItem);
            equippedItem = null;
        }
        else
        {
            CompareItem(equippedItem, newItem);
        }
    }

    private void CompareItem(ItemData equippedItem, ItemData newItem)
    {
        gameObject.SetActive(true);

        equippedItemInfo.ShowItem(equippedItem);
        newItemInfo.ShowItem(newItem);
        // TODO : 다른 버튼 비활성화 시켜주기 이후 아이템 선택을 하면 다시 다른 버튼 활성화 시켜주기
        // 이거 안해주니까 아이템 뽑기 버튼 계속 눌러주기가 가능했음 그 외에 다른 버튼이 또 있으면 마찬가지일것
    }

    public void OnClickBtn(int choice)
    {
        if(choice == 0)
        {
            ItemManager.itemManager.inventory.EquipItem(equippedItem);
        }
        else
        {
            ItemManager.itemManager.inventory.EquipItem(newItem);
        }
        gameObject.SetActive(false);
        this.newItem = null;
        equippedItem = null;
    }
}
