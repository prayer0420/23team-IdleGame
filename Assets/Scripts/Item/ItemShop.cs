using UnityEngine;

public class ItemShop : MonoBehaviour
{
    private ItemData[] itemDatas;
    private ItemData newItem;

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
