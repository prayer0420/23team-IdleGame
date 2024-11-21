using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    public Image itemImage;
    private TMP_Text nameText;
    private TMP_Text typeText;
    private TMP_Text statText;
    private TMP_Text descriptionText;

    private void Awake()
    {
        SetInfo();
        gameObject.SetActive(false);
    }

    private void SetInfo()
    {
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        itemImage.sprite = Resources.Load<Sprite>("Sprites/BasicItemSprite");
        nameText = transform.Find("Name").GetComponent<TMP_Text>();
        typeText = transform.Find("Type").GetComponent<TMP_Text>();
        statText = transform.Find("Stat").GetComponent<TMP_Text>();
        descriptionText = transform.Find("Description").GetComponent<TMP_Text>();
    }

    public void ShowItem(ItemData item)
    {
        if (item == null)
        {
            Debug.Log("������ �����Ͱ� �����ϴ�.");
            return;
        }

        gameObject.SetActive(true);

        if (itemImage == null)
        {
            SetInfo();
            gameObject.SetActive(true);
        }

        // sprite�� null�� ���¿��� ������ �־��ַ� �ϸ� nullReference�� �߻��Ѵ�.
        // �׷��� �� �ڵ带 ����ϱ� ���� Awake���� Spirte���� �⺻ sprite�� �߰������. -> �ٵ��� �ȵǳ�
        // �̰����� ����� ���庻 ��� ������ �������� �� UI�� ����ä�� �����ߴ���
        // ���ʿ� awake�� ������ �ȵƾ��� �� ���·� ShowItem�� ���� ����Ǵ�
        // itemImage���� null���� ������� �� �ۿ� ����.
        itemImage.sprite = item.itemSprite;
        nameText.text = item.name;
        typeText.text = item.itemType.ToString();
        statText.text = item.itemStat.ToString();
        descriptionText.text = item.itemDescription;
    }

    public void OnClickBtn()
    {
        gameObject.SetActive(false);
    }
}
