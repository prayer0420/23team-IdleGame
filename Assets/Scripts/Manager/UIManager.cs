using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Main UI")]
    [SerializeField] private TextMeshProUGUI chapterText;
    [SerializeField] private Button chapterButton;
    [SerializeField] private Image[] starImages; 

    [Header("Stage Select UI")]
    [SerializeField] private StageSelectUI stageSelectUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //InitializeUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public void InitializeUI()
    //{
    //    SetupListeners();
    //    UpdateUI(GameManager.Instance.CurrentChapter, GameManager.Instance.CurrentStage, GameManager.Instance.CurrentDifficulty);
    //    UpdateAllStars();
    //}
    private void Start()
    {
        SetupListeners();
    }
    private void SetupListeners()
    {
        chapterButton.onClick.AddListener(ToggleStageSelect);
    }

    public void UpdateUI(int chapter, int stage, DifficultyLevel difficulty)
    {
        chapterText.text = $"Chapter {chapter}";
        stageSelectUI.UpdateStageUI();
    }

    public void ToggleStageSelect()
    {
        stageSelectUI.ToggleStageSelect();
    }

    public void UpdateStarUI(int stageNumber, bool isCleared)
    {
        if (stageNumber <= 3)
        {
            for (int i = 0; i < stageNumber; i++)
            {
                string spritePath = isCleared ? "Sprites/Star_Filled" : "Sprites/Star_Empty";
                Sprite updateSprite = ResourceManager.Instance.LoadResource<Sprite>(spritePath);
                if (updateSprite != null)
                {
                    starImages[i].sprite = updateSprite;
                }
            }
        }
    }

    //게임 시작 할때(Load, newGame)
    private void UpdateAllStars()
    {
        ChapterData currentChapterData = GameManager.Instance.GetCurrentChapterData();
        for (int i = 0; i < starImages.Length; i++)
        {
            bool isCleared = currentChapterData.stages[i].isCleared;
            string spritePath = isCleared ? "Sprites/Star_Filled" : "Sprites/Star_Empty";
            Sprite starSprite = ResourceManager.Instance.LoadResource<Sprite>(spritePath);
            if (starSprite != null)
            {
                starImages[i].sprite = starSprite;
            }
        }
    }

    public void ResetStarUI()
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = Resources.Load<Sprite>("Sprites/Star_Empty");
        }
    }

    public void UnlockHardMode()
    {
        stageSelectUI.UnlockHardMode();
    }
}
