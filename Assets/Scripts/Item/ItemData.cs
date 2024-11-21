using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemID; // ���� �ĺ���
    public Sprite itemSprite;
    public string itemName;
    public ItemType itemType;
    public int itemStat;
    public string itemDescription;

}
