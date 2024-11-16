using UnityEngine;
using System.Collections.Generic;
using GameProject.Levels;
using GameProject.Characters;

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
        public List<Chapter> Chapters { get; private set; }
        private Player player;
        private UIManager uiManager;
        private Chapter currentChapter;
        private Stage currentStage;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeGame();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        //게임 초기화
        private void InitializeGame()
        {
            CurrentDifficulty = DifficultyLevel.Normal;
            player = Instantiate(Resources.Load<GameObject>("Prefabs/Player")).GetComponent<Player>();
            uiManager = GetComponent<UIManager>();
            InitializeChapters();

            currentChapter = Chapters[0];
            currentStage = currentChapter.Stages[0];
            StartStage(currentChapter.ChapterNumber, currentStage.StageNumber);
        }

        //챕터 초기화
        private void InitializeChapters()
        {
            Chapters = new List<Chapter>();
            for (int i = 1; i <= 3; i++)
            {
                GameObject chapterObj = new GameObject($"Chapter_{i}");
                Chapter chapter = chapterObj.AddComponent<Chapter>();
                chapter.InitializeChapter(i, player);
                Chapters.Add(chapter);
            }
        }

        public void StartStage(int chapterNumber, int stageNumber)
        {
            currentChapter = Chapters[chapterNumber - 1];
            currentStage = currentChapter.Stages[stageNumber - 1];
            currentStage.InitializeStage(stageNumber, stageNumber == 3, currentChapter.GetComponent<Map>(), player);
            uiManager.UpdateUI(chapterNumber, stageNumber);
        }

        public void CompleteStage()
        {
            currentStage.IsCleared = true;
            uiManager.UpdateStarUI(currentStage.StageNumber, true);
        }

        public void OnPlayerDeath()
        {
            uiManager.ShowDeathScreen();
            
            // 이전 스테이지로 돌아가기
            int previousStageIndex = currentChapter.Stages.IndexOf(currentStage) - 1;
            if (previousStageIndex >= 0)
            {
                StartStage(currentChapter.ChapterNumber, previousStageIndex + 1);
            }
            else
            {
                // 현재 챕터의 첫 스테이지라면 그대로 유지
                StartStage(currentChapter.ChapterNumber, currentStage.StageNumber);
            }
        }
    }


}
