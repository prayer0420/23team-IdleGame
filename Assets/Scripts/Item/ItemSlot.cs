using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemType itemType;
    public ItemData item;
    private Image image;

    private void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
        Color color = new Color(255, 255, 255, 0);
        image.color = color;
    }

    public void SetItem(ItemData item)
    {
        this.item = item;
        image.sprite = item.itemSprite;
        Color color = new Color(255, 255, 255, 255);
        image.color = color;
    }

    public void OnClickItem()
    {
        Debug.Log(item);
        Debug.Log(item.name);
        ItemManager.itemManager.uiItemInfo.ShowItem(item);
    }
}
