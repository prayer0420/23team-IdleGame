using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Data;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Main UI")]
    [SerializeField] public TextMeshProUGUI chapterText;
    [SerializeField] public Button chapterButton;
    [SerializeField] public Image[] TopstarImages;

    [Header("Stage Select UI")]
    [SerializeField] private ChapterSelectUI chapterSelectUI;
    [SerializeField] private Button nextStage;
    [SerializeField] private Button prevStage;

    //public Action<int, bool> OnStarUdpate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnStarUpdate += UpdateStarUI;

        chapterButton.onClick.AddListener(ToggleChapterSelect);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStarUpdate -= UpdateStarUI;
    }

    
    public void UpdateUI(int chapter, int stage, DifficultyLevel difficulty)
    {
        //Top업데이트
        chapterText.text = $"Chapter {chapter}";
        //UpdateStarUI(stag);
        UpdateAllStars(stage);

        //StagePanel창 업데이트
        chapterSelectUI.UpdateChapterUI();
    }

    public void ToggleChapterSelect()
    {
        chapterSelectUI.ToggleChapterPanelSelect();
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
                    TopstarImages[i].sprite = updateSprite;
                }
            }
        }
    }

    //게임 시작 할때(Load, newGame)
    private void UpdateAllStars(int stage)
    {
        ChapterData currentChapterData = GameManager.Instance.GetCurrentChapterData();
        for (int i = 0; i < stage-1; i++)
        {
            bool isCleared = currentChapterData.stages[i].isCleared;
            string spritePath = isCleared ? "Sprites/Star_Filled" : "Sprites/Star_Empty";
            Sprite starSprite = ResourceManager.Instance.LoadResource<Sprite>(spritePath);
            if (starSprite != null)
            {
                TopstarImages[i].sprite = starSprite;
            }
        }
    }

    public void ResetStarUI()
    {
        for (int i = 0; i < TopstarImages.Length; i++)
        {
            TopstarImages[i].sprite = ResourceManager.Instance.LoadResource<Sprite>("Sprites/Star_Empty");
        }
    }

    public void UnlockHardMode()
    {
        chapterSelectUI.ActivedifficultyToggleButton();
    }

    public void NextStage()
    {
        //현재 챕터 그대로
        int currentChapter = GameManager.Instance.CurrentChapter; 
        //현재 난이도 그대로
        DifficultyLevel difficultyLevel = GameManager.Instance.CurrentDifficulty;

        int currentStage = GameManager.Instance.CurrentStage;
        //챕터의 마지막 스테이지에서는 NextStage 못함(다음 챕터로 못감)
        if(currentStage >= GameManager.Instance.GetCurrentChapterData().stages.Length)
        {
            return;
        }
        GameManager.Instance.StartStage(currentChapter, currentStage+1, difficultyLevel, true);
    }


    public void PrevStage()
    {
        //현재 챕터 그대로
        int currentChapter = GameManager.Instance.CurrentChapter;
        //현재 난이도 그대로
        DifficultyLevel difficultyLevel = GameManager.Instance.CurrentDifficulty;

        int currentStage = GameManager.Instance.CurrentStage;
        //처음 스테이지에서는 PrevStage 못함(이전 챕터로 못감)
        if (currentStage == 1)
        {
            return;
        }
        GameManager.Instance.StartStage(currentChapter, currentStage -1, difficultyLevel, true);
    }

}
