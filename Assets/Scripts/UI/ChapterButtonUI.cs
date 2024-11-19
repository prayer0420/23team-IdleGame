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

        //챕터 버튼을 눌렀을때 해야할 일들
        //1. 맵 전환
        //2. 적 초기화 및 생성
        //3. Top UI, ChapterSelectUI 업데이트
        //4. 게임의 정보 업데이트
        //5. 어떤 정보를 기반으로 ? 이 ChapterButtonUI에 난이도, 챕터, 스테이지에 대한 정보가 있어야함
        //6. 이러한 정보를 이 ChapterButtonUI에 어떻게 저장할지가 고민
        //7. GameManager와 연계해서 코드 수정 필요
    }

}
