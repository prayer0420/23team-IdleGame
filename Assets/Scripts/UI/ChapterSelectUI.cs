using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChapterSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject stageSelectPanel;
    [SerializeField] private Transform stageButtonsContainer;
    [SerializeField] private GameObject stageButtonPrefab;
    [SerializeField] private Button difficultyToggleButton;
    [SerializeField] private TextMeshProUGUI difficultyText;

    private bool isHardMode = false;
    private ChapterButtonUI[] stageButtons;

    private void Awake()
    {

    }

    private void Start()
    {
        stageSelectPanel.SetActive(false);

        difficultyToggleButton.onClick.AddListener(ToggleDifficulty);
        difficultyToggleButton.interactable = false;

        UpdateDifficultyText();
    }

    public void ToggleStageSelect()
    {
        stageSelectPanel.SetActive(!stageSelectPanel.activeSelf);
        if (stageSelectPanel.activeSelf)
        {
            UpdateChapterUI();
        }
    }

    public void UpdateChapterUI()
    {
        UpdateChapterButtonsForCurrentChapter();
    }


    private void InitializeChapterButtons()
    {
        Debug.Log("stagebutton 초기화2");
        //버튼이 이미 있는경우엔 패스
        if (stageButtons != null && stageButtons.Length > 0)
        {
            return;
        }
        int maxStages = 3;
        stageButtons = new ChapterButtonUI[maxStages];

        for (int i = 0; i < maxStages; i++)
        {
            GameObject buttonObj = Instantiate(stageButtonPrefab, stageButtonsContainer);
            ChapterButtonUI chapterButtonUI = buttonObj.GetComponent<ChapterButtonUI>();
            //chapterButtonUI.Initialize();
            stageButtons[i] = chapterButtonUI;
        }
    }

    private void UpdateChapterButtonsForCurrentChapter()
    {
        if(stageButtons == null)
        {
            Debug.Log("stagebutton 초기화1");
            InitializeChapterButtons();
        }

        var progressData = GameManager.Instance.GetGameProgress();
        var currentChapters = isHardMode ? progressData.hardChapters : progressData.normalChapters;
        int currentChapterIndex = GameManager.Instance.CurrentChapter - 1;
        ChapterData currentChapter = currentChapters[currentChapterIndex];

        for (int i = 0; i < stageButtons.Length; i++)
        {
            ChapterButtonUI chapterButtonUI = stageButtons[i];
            chapterButtonUI.gameObject.SetActive(true);
            chapterButtonUI.UpdateChapterButtonUI(i+1, progressData.normalChapters[i]);
            chapterButtonUI.HandleStarUpdate(progressData.normalChapters[i],i);
        }
    }

    public void ToggleDifficulty()
    {
        isHardMode = !isHardMode;
        UpdateDifficultyText();
        GameManager.Instance.SetDifficulty(isHardMode ? DifficultyLevel.Hard : DifficultyLevel.Normal);
        UpdateChapterUI();
    }

    private void UpdateDifficultyText()
    {
        difficultyText.text = isHardMode ? "Hard Mode" : "Normal Mode";
    }

    public void UnlockHardMode()
    {
        difficultyToggleButton.interactable = true;
    }
}
