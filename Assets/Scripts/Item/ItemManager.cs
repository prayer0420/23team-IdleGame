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

    public void AddCharaterStat(ItemData item)
    {
        switch(item.itemType)
        {
            case ItemType.Weapon:
                GameManager.Instance.player.Data.playerData.BaseDamage += item.itemStat;
                break;
            case ItemType.Boots:
                GameManager.Instance.player.Data.playerData.BaseMaxHealth += item.itemStat;
                break;
            case ItemType.Helmet:
                GameManager.Instance.player.Data.playerData.BaseMaxHealth += item.itemStat;
                break;
            case ItemType.Gloves:
                GameManager.Instance.player.Data.playerData.BaseMaxHealth += item.itemStat;
                break;
            case ItemType.Armor:
                GameManager.Instance.player.Data.playerData.BaseMaxHealth += item.itemStat;
                break;
            default:
                Debug.Log("버그 발생");
                break;
        }
    }

    public void SubtractCharaterStat(ItemData item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                GameManager.Instance.player.Data.playerData.BaseDamage -= item.itemStat;
                break;
            case ItemType.Boots:
                GameManager.Instance.player.Data.playerData.BaseMaxHealth -= item.itemStat;
                break;
            case ItemType.Helmet:
                GameManager.Instance.player.Data.playerData.BaseMaxHealth -= item.itemStat;
                break;
            case ItemType.Gloves:
                GameManager.Instance.player.Data.playerData.BaseMaxHealth -= item.itemStat;
                break;
            case ItemType.Armor:
                GameManager.Instance.player.Data.playerData.BaseMaxHealth -= item.itemStat;
                break;
            default:
                Debug.Log("버그 발생");
                break;
        }
    }
}
