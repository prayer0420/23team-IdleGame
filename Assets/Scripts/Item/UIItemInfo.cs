using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{

    private Image itemImage;
    private TMP_Text nameText;
    private TMP_Text typeText;
    private TMP_Text statText;
    private TMP_Text descriptionText;

    private void Awake()
    {
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        nameText = transform.Find("Name").GetComponent<TMP_Text>();
        typeText = transform.Find("Type").GetComponent<TMP_Text>();
        statText = transform.Find("Stat").GetComponent<TMP_Text>();
        descriptionText = transform.Find("Description").GetComponent<TMP_Text>();

        gameObject.SetActive(false);
    }

    public void ShowItem(ItemData item)
    {
        Debug.Log("아이템 무야 " + item);
        Debug.Log("이거 뜨냐? 11" + item.itemSprite);
        Debug.Log("이거 뜨냐? 22" + itemImage.sprite);
        if (item != null)
        {
            gameObject.SetActive(true);

            itemImage.sprite = item.itemSprite;
            nameText.text = item.name;
            typeText.text = item.itemType.ToString();
            statText.text = item.itemStat.ToString();
            descriptionText.text = item.itemDescription;
        }
    }

    public void OnClickBtn()
    {
        gameObject.SetActive(false);
    }
}
