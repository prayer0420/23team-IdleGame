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
        // 처음엔 장착한 아이템이 없어서 image가 없기 때문에 color가 나온다. 그러니 투명도를 통해 안보이게 해준다.
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
