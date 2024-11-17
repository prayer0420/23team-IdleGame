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
            // MapManager ����
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
            // �÷��̾� ����
            GameObject playerObj = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
            player = playerObj.GetComponent<Player>();

            //uiManager = new GameObject("UIManager").AddComponent<UIManager>();
            uiManager.InitializeUI(INITNUMBER);

            stageManager.InitializeStageManager(player);
            
            StartStage(CurrentChapter, CurrentStage);
        }

        public void StartStage(int chapterNumber, int stageNumber)
        {

            // �� ������Ʈ(���̵���-�ƿ�, ���ο� �� ����)
            mapManager.ChangeMap(chapterNumber, () => { stageManager.StartStage(chapterNumber, stageNumber); });
            Debug.Log("�ʾ���");

            // �������� ����(�� ����)
            //stageManager.StartStage(chapterNumber, stageNumber);
            Debug.Log("�������� �����");

            // UI ������Ʈ
            uiManager.UpdateChapterUI(chapterNumber);
            Debug.Log("é�;�����Ʈ");

            uiManager.UpdateStageUI(stageNumber);
            Debug.Log("��������������Ʈ");

            CurrentChapter = chapterNumber;
            CurrentStage = stageNumber;
        }


        //�������� Ŭ���� ��
        public void OnStageCleared()
        {
            //���� �����ʱ��� Ŭ�����Ѱ� �ƴ϶��
            if (CurrentStage < 3)
            {
                // ���� ���������� �̵�
                CurrentStage++;
                Debug.Log($"{CurrentStage} ���������� �̵�");

                StartStage(CurrentChapter, CurrentStage);
            }
            else //�����ʱ��� Ŭ���� �ߴٸ�
            {
                // �븻 3é���� 3������������ Ŭ�����Ѱ� �ƴ϶��
                if (CurrentChapter < 3)
                {
                    // ���� é�ͷ� �̵�
                    CurrentChapter++;
                    Debug.Log($"{CurrentChapter}é�ͷ� �̵�");
                    //���������� 1���� �ٽ� ����
                    CurrentStage = 1;
                    StartStage(CurrentChapter, CurrentStage);
                }
                //�븻 3é���� 3���������� Ŭ�����Ѱ��̶��
                else
                {
                    // �븻�� ��� é�� Ŭ���� �� ���̹Ƿ� 
                    if (CurrentDifficulty == DifficultyLevel.Normal)
                    {
                        // ����� ���̵� �ر�
                        CurrentDifficulty = DifficultyLevel.Hard;
                        CurrentChapter = 1;
                        CurrentStage = 1;
                        uiManager.UnlockDifficultyUI(CurrentDifficulty);
                        Debug.Log("����� ���̵� �ر�!");
                    }
                    //������� ��� é�͸� Ŭ�����ϸ� ��~
                    else
                    {
                        Debug.Log("��� ���̵��� ��� é�͸� Ŭ�����߽��ϴ�!");
                    }
                }
            }
            uiManager.UpdateStarUI(CurrentStage, true);
            Debug.Log("�������� Ŭ����");
        }

        public void OnPlayerDeath()
        {
            // ��� ó�� �� ���� ���������� �̵�
            if (CurrentStage > 1)
            {
                CurrentStage--;
            }
            StartStage(CurrentChapter, CurrentStage);
            Debug.Log("���� �������� ����");
        }
    }
}
