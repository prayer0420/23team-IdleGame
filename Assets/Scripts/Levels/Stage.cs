using UnityEngine;
using GameProject.Characters;
using GameProject.Managers;

namespace GameProject.Levels
{
    public class Stage : MonoBehaviour
    {
        public int StageNumber { get; private set; }
        public bool IsCleared { get; set; }
        public bool IsBossStage { get; private set; }
        private Map map;
        private Player player;
        private Enemy[] enemies;


        private void Update()
        {
            CheckStageClear();
        }

        public void InitializeStage(int stageNumber, bool isBossStage, Map map, Player player)
        {
            StageNumber = stageNumber;
            IsBossStage = isBossStage;
            this.map = map;
            this.player = player;
            IsCleared = false;

            // 몬스터 스폰
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            int enemyCount = IsBossStage ? 1 : 5;
            enemies = new Enemy[enemyCount];
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemyObj = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"));
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                enemyObj.transform.position = new Vector2(700, Random.Range(200, 1080)); // 오른쪽에서 스폰
                enemies[i] = enemy;
            }
        }

        public void CheckStageClear()
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null && enemy.IsAlive)
                {
                    return;
                }
            }
            IsCleared = true;
            GameManager.Instance.CompleteStage();
        }
    }
}
