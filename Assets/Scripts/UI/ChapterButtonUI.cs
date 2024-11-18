using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chapterNumberText;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Button button;
    [SerializeField] private Image[] starImages;

    private bool isSubscribed;

    public void Start()
    {
        //GameManager.Instance.OnStarUpdate+= HandleStarUpdate;

    }

    //public void Initialize()
    //{
    //    if (!isSubscribed)
    //    {
    //        GameManager.Instance.OnStarUpdate += HandleStarUpdate;
    //        isSubscribed = true;
    //    }
    //}

    //private void OnDestroy()
    //{
    //    if (isSubscribed && GameManager.Instance != null)
    //    {
    //        GameManager.Instance.OnStarUpdate -= HandleStarUpdate;
    //        isSubscribed = false;
    //    }
    //}

    public void UpdateChapterButtonUI(int chapterNumber, ChapterData data)
    {
        chapterNumberText.text = chapterNumber.ToString();

        UpdateUI(data);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            GameManager.Instance.StartStage(chapterNumber, true);
        });
    }


    public void HandleStarUpdate(ChapterData chapterdata, int index)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            Debug.Log("HandleStarUpdate");
            string spritePath = chapterdata.stages[i].isCleared ? "Sprites/Star_Filled" : "Sprites/Star_Empty";
            Sprite updateSprite = ResourceManager.Instance.LoadResource<Sprite>(spritePath);
            if (updateSprite != null)
            {
                starImages[i].sprite = updateSprite;
            }
        }
    }
    public void UpdateUI(ChapterData data)
    {
        lockIcon.SetActive(!data.isUnlocked);
        button.interactable = data.isUnlocked;

        //string spritePath = data.isCleared ? "Sprites/Star_Filled" : "Sprites/Star_Empty";
        //Sprite starSprite = ResourceManager.Instance.LoadResource<Sprite>(spritePath);
        //if (starSprite != null)
        //{
        //    foreach (var starImg in starImages)
        //    {
        //        starImg.sprite = starSprite;
        //    }
        //}
    }

}
