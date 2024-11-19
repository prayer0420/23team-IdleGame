using System;
using UnityEngine;

public enum DifficultyLevel
{
    Normal,
    Hard
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int CurrentChapter { get; private set; }
    public int CurrentStage { get; private set; }
    public DifficultyLevel CurrentDifficulty { get; private set; }

    private const int INITNUMBER = 1;

    private SaveData saveData;
    private TestPlayer player;
    private UIManager uiManager;
    private MapManager mapManager;
    private StageManager stageManager;

    public Action<int,bool> OnStarUpdate;

    public bool isPause;

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
    }

    public void InitializeGame()
    {
        saveData = SaveManager.Instance.LoadGame();

        if (saveData == null)
        {
            saveData = new SaveData();
        }


        CurrentChapter = saveData.currentChapter;
        CurrentStage = saveData.currentStage;
        CurrentDifficulty = saveData.difficulty;

        uiManager = UIManager.Instance;
        mapManager = MapManager.Instance;
        stageManager = StageManager.Instance;

        CreatePlayer();
        InitializeManagers();
        StartStage(CurrentChapter, CurrentStage, CurrentDifficulty, false);
    }

    private void CreatePlayer()
    {
        GameObject playerPrefab = ResourceManager.Instance.LoadResource<GameObject>("Prefabs/TestPlayer");
        if (playerPrefab != null)
        {
            GameObject playerObj = Instantiate(playerPrefab);
            player = playerObj.GetComponent<TestPlayer>();
        }
    }


    private void InitializeManagers()
    {
        stageManager.InitializeStageManager(player);
    }

    public void StartStage(int chapterNumber, int stageNumber, DifficultyLevel difficulty, bool isFade)
    {
        ChapterData currentChapterData = GetCurrentChapterData();
        if (!currentChapterData.stages[stageNumber - 1].isUnlocked)
        {
            Debug.Log("아직 잠겨있는 스테이지입니다.");
            return;
        }

        // 상태 업데이트
        CurrentChapter = chapterNumber;
        CurrentStage = stageNumber;
        CurrentDifficulty = difficulty;


        StageManager.Instance.StopAllCoroutines();
        //맵 전환
        mapManager.ChangeMap(CurrentChapter, () =>
        {
            //적 초기화
            stageManager.StartStage(CurrentChapter, CurrentStage, CurrentDifficulty);
        }, isFade);

        //UI업데이트
        uiManager.ResetStarUI();
        uiManager.UpdateUI(CurrentChapter, CurrentStage, CurrentDifficulty);

        SaveGame();
    }

    public void OnStageCleared()
    {
        Debug.Log("스테이지 클리어");
        ChapterData currentChapterData = GetCurrentChapterData();
        // 스테이지 데이타에서 현재 스테이지를 클리어로 표시
        StageData currentStageData = currentChapterData.stages[CurrentStage - 1];
        currentStageData.isCleared = true;


        // STAR 업데이트
        OnStarUpdate?.Invoke(CurrentStage,true);
        //uiManager.UpdateStarUI(CurrentStage, true);

        // 다음 스테이지 해금
        if (CurrentStage < 3)
        {
            CurrentStage++;
            currentChapterData.stages[CurrentStage - 1].isUnlocked = true;
        }
        //3스테이지 클리어시 -> 챕터완료
        else
        {
            // 챕터 완료 시 다음 챕터로 이동하고 STAR 초기화
            if (CurrentChapter < 3)
            {
                CurrentChapter++; //챕터+1
                CurrentStage = 1; //스테이지 초기화
                ChapterData nextChapterData = GetCurrentChapterData();
                //다음 챕터, 스테이지 해금
                nextChapterData.isUnlocked = true;
                nextChapterData.stages[CurrentStage-1].isUnlocked = true;

                // STAR UI 초기화
                uiManager.ResetStarUI();
            }
            //3챕터까지 완료시
            else
            {
                //하드모드로 전환
                if (CurrentDifficulty == DifficultyLevel.Normal)
                {
                    CurrentDifficulty = DifficultyLevel.Hard;
                    CurrentChapter = 1;
                    CurrentStage = 1;

                    // 하드 모드의 첫 번째 챕터와 스테이지 해금
                    saveData.progress.hardChapters[0].isUnlocked = true;
                    saveData.progress.hardChapters[0].stages[0].isUnlocked = true;

                    // 하드 모드 해금 및 STAR UI 초기화
                    uiManager.UnlockHardMode();
                    Debug.Log("하드모드 해금");
                    uiManager.ResetStarUI();
                }
                else
                {
                    // 게임 클리어 처리
                    Debug.Log("게임클리어...");
                }
            }
        }

        //SaveGame();
        //uiManager.UpdateUI(CurrentChapter, CurrentStage, CurrentDifficulty);
        Debug.Log("다음 스테이지 이동");
        StartStage(CurrentChapter,CurrentStage, CurrentDifficulty,true);
    }

    public ChapterData GetCurrentChapterData()
    {
        return CurrentDifficulty == DifficultyLevel.Normal ?
            saveData.progress.normalChapters[CurrentChapter - 1] :
            saveData.progress.hardChapters[CurrentChapter - 1];
    }

    public void SaveGame()
    {
        //TODO : Current뿐 아니라 전체 다 받아와야함
        saveData.currentChapter = CurrentChapter;
        saveData.currentStage = CurrentStage;
        saveData.difficulty = CurrentDifficulty;

        SaveManager.Instance.SaveGame(saveData);
    }

    public GameProgressData GetGameProgress()
    {
        return saveData.progress;
    }

    //public void SetDifficulty(DifficultyLevel difficulty)
    //{
    //    CurrentDifficulty = difficulty;
    //    SaveGame();
    //}

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
