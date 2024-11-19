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
        // GameManager�� ���� ���� ���� ������Ʈ �� �������� ����
        GameManager.Instance.StartStage(chapterNumber, stageNumber, difficultyLevel, true);

        // ChapterSelectUI �г� �ݱ�
        UIManager.Instance.ToggleChapterSelect();

        //é�� ��ư�� �������� �ؾ��� �ϵ�
        //1. �� ��ȯ
        //2. �� �ʱ�ȭ �� ����
        //3. Top UI, ChapterSelectUI ������Ʈ
        //4. ������ ���� ������Ʈ
        //5. � ������ ������� ? �� ChapterButtonUI�� ���̵�, é��, ���������� ���� ������ �־����
        //6. �̷��� ������ �� ChapterButtonUI�� ��� ���������� ���
        //7. GameManager�� �����ؼ� �ڵ� ���� �ʿ�
    }

}
