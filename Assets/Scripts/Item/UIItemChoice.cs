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
        // TODO : �ٸ� ��ư ��Ȱ��ȭ �����ֱ� ���� ������ ������ �ϸ� �ٽ� �ٸ� ��ư Ȱ��ȭ �����ֱ�
        // �̰� �����ִϱ� ������ �̱� ��ư ��� �����ֱⰡ �������� �� �ܿ� �ٸ� ��ư�� �� ������ ���������ϰ�
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
