using UnityEngine;

public class UIItemChoice : MonoBehaviour
{
    [SerializeField] private UIItemInfo equippedItemInfo;
    [SerializeField] private UIItemInfo newItemInfo;
    private ItemData equippedItem;
    private ItemData newItem;

    private void Awake()
    {
        equippedItemInfo = transform.Find("EquippedItemInfo").GetComponent<UIItemInfo>();
        newItemInfo = transform.Find("NewItemInfo").GetComponent<UIItemInfo>();

        equippedItemInfo.gameObject.SetActive(true);
        newItemInfo.gameObject.SetActive(true);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ChooseItem(ItemData newItem)
    {
        this.newItem = newItem;
        equippedItem = ItemManager.itemManager.inventory.GetItem(newItem.itemType);
        if (equippedItem == null)
        {
            ItemManager.itemManager.inventory.EquipItem(newItem);
            ItemManager.itemManager.AddCharaterStat(newItem);
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
        if(choice == 1)
        {
            ItemManager.itemManager.SubtractCharaterStat(equippedItem);
            ItemManager.itemManager.inventory.EquipItem(newItem);
            ItemManager.itemManager.AddCharaterStat(newItem);
        }

        Debug.Log("���ݷ� : " + GameManager.Instance.player.Data.playerData.BaseDamage);
        Debug.Log("ü�� : " + GameManager.Instance.player.Data.playerData.BaseMaxHealth);

        gameObject.SetActive(false);
        this.newItem = null;
        equippedItem = null;
    }
}
