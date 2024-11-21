using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChapterSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject ChapterSelectPanel;
    [SerializeField] private Transform chapterButtonsContainer;
    [SerializeField] private Button difficultyToggleButton;
    [SerializeField] private TextMeshProUGUI difficultyText;

    private GameObject chapterButtonPrefab;
    private bool isHardMode = false;
    private ChapterButtonUI[] chapterButtons;


    private void Start()
    {
        ChapterSelectPanel.SetActive(false);

        difficultyToggleButton.onClick.AddListener(ToggleDifficulty);
        difficultyToggleButton.interactable = false;
    }

    //Open ChapterPnael
    public void ToggleChapterPanelSelect()
    {
        ChapterSelectPanel.SetActive(!ChapterSelectPanel.activeSelf);
        if (ChapterSelectPanel.activeSelf)
        {
            //Debug.Log("챕터 패널 창 열었을 때");
            UpdateDifficultyText();
            if (GameManager.Instance.CurrentDifficulty == DifficultyLevel.Hard)
            {
                //difficultyToggleButton 활성화
                ActivedifficultyToggleButton();
            }
            UpdateChapterUI();
        }
    }

    public void UpdateChapterUI()
    {
        UpdateChapterButtonsForCurrentChapter();
    }


    private void InitializeChapterButtons()
    {
        //버튼이 이미 있는경우엔 패스
        if (chapterButtons != null && chapterButtons.Length > 0)
        {
            return;
        }
        int maxStages = 3;
        chapterButtons = new ChapterButtonUI[maxStages];

        string prefabPath = $"Prefabs/Stage/ChapterButton";

        for (int i = 0; i < maxStages; i++)
        {
            GameObject buttonPrefab = ResourceManager.Instance.LoadResource<GameObject>(prefabPath);
            GameObject buttonObj = Instantiate(buttonPrefab, chapterButtonsContainer);

            ChapterButtonUI chapterButtonUI = buttonObj.GetComponent<ChapterButtonUI>();
            //chapterButtonUI.Initialize();
            chapterButtons[i] = chapterButtonUI;
        }
    }

    private void UpdateChapterButtonsForCurrentChapter()
    {

        if(chapterButtons == null)
        {
            InitializeChapterButtons();
        }
        var progressData = GameManager.Instance.GetGameProgress();
        
        // isHard에 따라 사용할 챕터 배열을 선택
        var chapters = isHardMode ? progressData.hardChapters : progressData.normalChapters;

        for (int i = 0; i < chapterButtons.Length; i++)
        {
            ChapterButtonUI chapterButtonUI = chapterButtons[i];
            chapterButtonUI.gameObject.SetActive(true);

            DifficultyLevel difficulty = isHardMode ? DifficultyLevel.Hard : DifficultyLevel.Normal;
            chapterButtonUI.UpdateChapterButtonUI(1, chapters[i], difficulty, i+1);
            chapterButtonUI.HandleStarUpdate(chapters[i]);
        }

    }

    //DifficultyButton눌렸을 때
    public void ToggleDifficulty()
    {
        isHardMode = !isHardMode; //처음 Normal -> Hard로 변경(false에서 true로)
        UpdateDifficultyText(); //text도 바꿈

        UpdateChapterUI();
    }

    private void UpdateDifficultyText()
    {
        difficultyText.text = isHardMode ? "Hard Mode" : "Normal Mode";
    }

    public void ActivedifficultyToggleButton()
    {
        difficultyToggleButton.interactable = true;
    }
}
