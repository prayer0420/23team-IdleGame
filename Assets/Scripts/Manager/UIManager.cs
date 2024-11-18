using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Main UI")]
    [SerializeField] public TextMeshProUGUI chapterText;
    [SerializeField] public Button chapterButton;
    [SerializeField] public Image[] TopstarImages;

    [Header("Stage Select UI")]
    [SerializeField] private ChapterSelectUI chapterSelectUI;

    public Action<int, bool> OnStarUdpate;

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
        chapterText.text = $"Chapter {chapter}";
        //StagePanel창 업데이트
        chapterSelectUI.UpdateChapterUI();
    }

    public void ToggleChapterSelect()
    {
        chapterSelectUI.ToggleStageSelect();
    }

    public void UpdateStarUI(int stageNumber, bool isCleared)
    {
        //OnStarUdpate?.Invoke(stageNumber, isCleared);
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
    private void UpdateAllStars()
    {
        ChapterData currentChapterData = GameManager.Instance.GetCurrentChapterData();
        for (int i = 0; i < TopstarImages.Length; i++)
        {
            bool isCleared = currentChapterData.stages[i].isCleared;
            string spritePath = isCleared ? "Sprites/Star_Filled" : "Sprites/Star_Empty";
            Sprite starSprite = ResourceManager.Instance.LoadResource<Sprite>(spritePath);
            if (starSprite != null)
            {
                TopstarImages[i].sprite = starSprite;
                Debug.Log("3");
                OnStarUdpate?.Invoke(0, isCleared);
            }
        }
    }

    public void ResetStarUI()
    {
        for (int i = 0; i < TopstarImages.Length; i++)
        {
            TopstarImages[i].sprite = Resources.Load<Sprite>("Sprites/Star_Empty");
            OnStarUdpate?.Invoke(0, false);
        }
    }

    public void UnlockHardMode()
    {
        chapterSelectUI.UnlockHardMode();
    }
}
