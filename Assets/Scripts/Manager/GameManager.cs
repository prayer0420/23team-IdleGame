using UnityEngine;
using GameProject.Characters;
using GameProject.Levels;
using GameProject.UI;
using UnityEngine.UIElements.Experimental;

namespace GameProject.Managers
{
    public enum DifficultyLevel
    {
        Normal,
        Hard
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public DifficultyLevel CurrentDifficulty { get; private set; }

        private Player player;
        private UIManager uiManager;
        private MapManager mapManager;
        private StageManager stageManager;
        public const int INITNUMBER = 1;

        public int CurrentChapter { get; private set; }
        public int CurrentStage { get; private set; }

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
            // MapManager 설정
            mapManager = MapManager.Instance;
            stageManager = StageManager.Instance;
            uiManager = UIManager.Instance;


            InitializeGame();
        }

        private void InitializeGame()
        {
            CurrentDifficulty = DifficultyLevel.Normal;
            CurrentChapter = INITNUMBER;
            CurrentStage = INITNUMBER;
            // 플레이어 생성
            GameObject playerObj = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
            player = playerObj.GetComponent<Player>();

            //uiManager = new GameObject("UIManager").AddComponent<UIManager>();
            uiManager.InitializeUI(INITNUMBER);

            stageManager.InitializeStageManager(player);
            
            StartStage(CurrentChapter, CurrentStage);
        }

        public void StartStage(int chapterNumber, int stageNumber)
        {

            // 맵 업데이트(페이드인-아웃, 새로운 맵 설정)
            mapManager.ChangeMap(chapterNumber, () => { stageManager.StartStage(chapterNumber, stageNumber); });
            Debug.Log("맵업뎃");

            // 스테이지 시작(적 생성)
            //stageManager.StartStage(chapterNumber, stageNumber);
            Debug.Log("스테이지 재시작");

            // UI 업데이트
            uiManager.UpdateChapterUI(chapterNumber);
            Debug.Log("챕터업데이트");

            uiManager.UpdateStageUI(stageNumber);
            Debug.Log("스테이지업데이트");

            CurrentChapter = chapterNumber;
            CurrentStage = stageNumber;
        }


        //스테이지 클리어 시
        public void OnStageCleared()
        {
            //아직 보스맵까지 클리어한게 아니라면
            if (CurrentStage < 3)
            {
                // 다음 스테이지로 이동
                CurrentStage++;
                Debug.Log($"{CurrentStage} 스테이지로 이동");

                StartStage(CurrentChapter, CurrentStage);
            }
            else //보스맵까지 클리어 했다면
            {
                // 노말 3챕터의 3스테이지까지 클리어한게 아니라면
                if (CurrentChapter < 3)
                {
                    // 다음 챕터로 이동
                    CurrentChapter++;
                    Debug.Log($"{CurrentChapter}챕터로 이동");
                    //스테이지는 1부터 다시 시작
                    CurrentStage = 1;
                    StartStage(CurrentChapter, CurrentStage);
                }
                //노말 3챕터의 3스테이지를 클리어한것이라면
                else
                {
                    // 노말의 모든 챕터 클리어 한 것이므로 
                    if (CurrentDifficulty == DifficultyLevel.Normal)
                    {
                        // 어려움 난이도 해금
                        CurrentDifficulty = DifficultyLevel.Hard;
                        CurrentChapter = 1;
                        CurrentStage = 1;
                        uiManager.UnlockDifficultyUI(CurrentDifficulty);
                        Debug.Log("어려움 난이도 해금!");
                    }
                    //어려움의 모듭 챕터를 클리어하면 끝~
                    else
                    {
                        Debug.Log("모든 난이도의 모든 챕터를 클리어했습니다!");
                    }
                }
            }
            uiManager.UpdateStarUI(CurrentStage, true);
            Debug.Log("스테이지 클리어");
        }

        public void OnPlayerDeath()
        {
            // 사망 처리 및 이전 스테이지로 이동
            if (CurrentStage > 1)
            {
                CurrentStage--;
            }
            StartStage(CurrentChapter, CurrentStage);
            Debug.Log("이전 스테이지 진행");
        }
    }
}
