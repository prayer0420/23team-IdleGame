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
    //private TestPlayer player;
    private UIManager uiManager;
    private MapManager mapManager;
    private StageManager stageManager;

    public Action<int,bool> OnStarUpdate;

    public bool isPause;

    public Player player;

    public bool isPuase;

    private Vector2 PlayerInitPosition = new Vector2(-5.72f, -1f);

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

    public void InitializeGame()
    {
        saveData = SaveManager.Instance.LoadGame();
        var itemManager = ItemManager.itemManager;

        if (saveData == null)
        {
            saveData = new SaveData();

            CreatePlayer();

            if (player != null)
            {
                saveData.playerSaveData = new PlayerSaveData(player.Data, player.Data.playerData.BaseMaxHealth);
            }
            else
            {
                // 플레이어를 생성할 수 없는 경우 기본값 설정
                PlayerSO defaultPlayerSO = ResourceManager.Instance.LoadResource<PlayerSO>("Data/Player");
                saveData.playerSaveData = new PlayerSaveData(defaultPlayerSO, defaultPlayerSO.playerData.BaseMaxHealth);
            }
            // 인벤토리 데이터 초기화
            saveData.inventoryData = new InventoryData(5);
        }
        else
        {
            CreatePlayer(); 
        }

        CurrentChapter = saveData.currentChapter;
        CurrentStage = saveData.currentStage;
        CurrentDifficulty = saveData.difficulty;

        uiManager = UIManager.Instance;
        mapManager = MapManager.Instance;
        stageManager = StageManager.Instance;


        // 플레이어의 스탯을 저장된 데이터로 초기화
        if (saveData.playerSaveData != null)
        {
            player.SetSaveData(saveData.playerSaveData);
        }

        //아이템 설정
        ItemManager.itemManager.inventory.SetInventoryData(saveData.inventoryData);

        stageManager.InitializeStageManager(player);

        StartStage(CurrentChapter, CurrentStage, CurrentDifficulty, false);
    }

    private void CreatePlayer()
    {
        GameObject playerPrefab = ResourceManager.Instance.LoadResource<GameObject>("Prefabs/Player");
        if (playerPrefab != null)
        {
            GameObject playerObj = Instantiate(playerPrefab, PlayerInitPosition, Quaternion.identity);
            player = playerObj.GetComponent<Player>();
        }
        player.PlayerOnDeath += HandlePlayerOnDeath;
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

        //플레이어 위치 초기화
        player.transform.position = PlayerInitPosition;

        stageManager.StartStage(CurrentChapter, CurrentStage, CurrentDifficulty);
        //맵 전환
        mapManager.ChangeMap(CurrentChapter, () =>
        {
            //적 초기화
            //stageManager.StartStage(CurrentChapter, CurrentStage, CurrentDifficulty);
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
        saveData.currentChapter = CurrentChapter;
        saveData.currentStage = CurrentStage;
        saveData.difficulty = CurrentDifficulty;

        // 플레이어 스탯 저장
        saveData.playerSaveData = new PlayerSaveData(player.Data, player.healthSystem.player.currentValue);

        // 인벤토리 데이터 저장
        saveData.inventoryData = ItemManager.itemManager.inventory.GetInventoryData();

        SaveManager.Instance.SaveGame(saveData);
    }

    public GameProgressData GetGameProgress()
    {
        return saveData.progress;
    }

    //플레이어 죽으면
    public void HandlePlayerOnDeath()
    {
        player.PlayerOnDeath -= HandlePlayerOnDeath;
        //플레이어 재생성
        CreatePlayer();
        //현재 스테이지 재시작
        StartStage(CurrentChapter, CurrentStage, CurrentDifficulty,true);
    }


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
