using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject stageSelectPanel;
    [SerializeField] private Transform stageButtonsContainer;
    [SerializeField] private GameObject stageButtonPrefab;
    [SerializeField] private Button difficultyToggleButton;
    [SerializeField] private TextMeshProUGUI difficultyText;

    private bool isHardMode = false;
    private StageButtonUI[] stageButtons;

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
            UpdateStageUI();
        }
    }

    public void UpdateStageUI()
    {
        InitializeStageButtons();
    }

    private void InitializeStageButtons()
    {
        // 기존 버튼들 제거
        foreach (Transform child in stageButtonsContainer)
        {
            Destroy(child.gameObject);
        }

        // 현재 챕터의 스테이지 데이터 가져오기
        var progressData = GameManager.Instance.GetGameProgress();
        var currentChapters = isHardMode ? progressData.hardChapters : progressData.normalChapters;
        int currentChapterIndex = GameManager.Instance.CurrentChapter - 1;
        ChapterData currentChapter = currentChapters[currentChapterIndex];

        stageButtons = new StageButtonUI[currentChapter.stages.Length];

        for (int i = 0; i < currentChapter.stages.Length; i++)
        {
            GameObject buttonObj = Instantiate(stageButtonPrefab, stageButtonsContainer);
            StageButtonUI stageButton = buttonObj.GetComponent<StageButtonUI>();
            stageButton.Initialize(i + 1, currentChapter.stages[i]);
            stageButtons[i] = stageButton;
        }
    }

    public void ToggleDifficulty()
    {
        isHardMode = !isHardMode;
        UpdateDifficultyText();
        GameManager.Instance.SetDifficulty(isHardMode ? DifficultyLevel.Hard : DifficultyLevel.Normal);
        UpdateStageUI();
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
