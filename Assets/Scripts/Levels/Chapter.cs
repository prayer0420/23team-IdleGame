using UnityEngine;
using System.Collections.Generic;
using GameProject.Characters;
using UnityEditor.SceneManagement;

namespace GameProject.Levels
{
    public class Chapter : MonoBehaviour
    {
        public int ChapterNumber { get; private set; }
        public List<Stage> Stages { get; private set; }
        public bool IsUnlocked { get; set; }
        private Map map;
        private Player player;

        public void InitializeChapter(int chapterNumber, Player player)
        {
            ChapterNumber = chapterNumber;
            this.player = player;
            IsUnlocked = (chapterNumber == 1); // 1챕터는 기본적으로 해금

            map = GetComponent<Map>();
            map.LoadMap(ChapterNumber);

            InitializeStages();
        }

        private void InitializeStages()
        {
            Stages = new List<Stage>();
            for (int i = 1; i <= 3; i++)
            {
                bool isBossStage = (i == 3);
                GameObject stageObj = new GameObject($"Stage_{i}");
                Stage stage = stageObj.AddComponent<Stage>();
                stage.InitializeStage(i, isBossStage, map, player);
                Stages.Add(stage);
            }
        }

        public bool IsCleared()
        {
            foreach (Stage stage in Stages)
            {
                if (!stage.IsCleared)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
