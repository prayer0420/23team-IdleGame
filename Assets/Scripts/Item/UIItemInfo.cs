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
            Debug.Log("아이템 데이터가 없습니다.");
            return;
        }

        gameObject.SetActive(true);

        if (itemImage == null)
        {
            SetInfo();
            gameObject.SetActive(true);
        }

        // sprite가 null인 상태에서 뭔가를 넣어주려 하면 nullReference가 발생한다.
        // 그래서 이 코드를 사용하기 위해 Awake에서 Spirte에는 기본 sprite를 추가해줬다. -> 근데도 안되네
        // 이것저것 방법을 차장본 결과 게임을 실행했을 때 UI가 꺼진채로 실행했더니
        // 애초에 awake가 실행이 안됐었다 그 상태로 ShowItem이 먼저 실행되니
        // itemImage에는 null값이 들어있을 수 밖에 없다.
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
