using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemID; // 고유 식별자
    public Sprite itemSprite;
    public string itemName;
    public ItemType itemType;
    public int itemStat;
    public string itemDescription;

}
