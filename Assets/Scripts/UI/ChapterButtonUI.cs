using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chapterNumberText;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Button button;
    [SerializeField] private Image[] starImages;

    private int chapterNumber;
    private int stageNumber;
    private DifficultyLevel difficultyLevel;
    private ChapterData chapterData;

    public void UpdateChapterButtonUI(int stageNumber, ChapterData data, DifficultyLevel difficulty, int chapterNumber)
    {
        this.stageNumber = stageNumber;
        this.chapterData = data;
        this.difficultyLevel = difficulty;
        this.chapterNumber = chapterNumber;


        chapterNumberText.text = chapterNumber.ToString();

        UpdateUI(data);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClickChapterButton);

    }

    public void HandleStarUpdate(ChapterData chapterdata)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
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
    }

    public void OnClickChapterButton()
    {
        // GameManager를 통해 현재 상태 업데이트 및 스테이지 시작
        GameManager.Instance.StartStage(chapterNumber, stageNumber, difficultyLevel, true);

        // ChapterSelectUI 패널 닫기
        UIManager.Instance.ToggleChapterSelect();
    }

}
