using UnityEngine;
using System.Collections;
using GameProject.Characters;
using GameProject.Managers;
using GameProject.UI;

namespace GameProject.Levels
{
    public class StageManager : MonoBehaviour
    {
        public static StageManager Instance { get; private set; }

        private Player player;
        [SerializeField] public Enemy[] enemies;
        private int Deathcount = 0;
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

        //public void SubScribeAction(Enemy enemy)
        //{
        //    enemy.OnDeath += CheckStageClear;
        //}
        public void DeSubScribeAction(Enemy enemy)
        {
            enemy.OnDeath -= HandleOnEnemyDeath;
            Debug.Log("구독해제");
        }

        public void InitializeStageManager(Player player)
        {
            this.player = player;
        }
        public void StartStage(int chapterNumber, int stageNumber)
        {
            // 이전 스테이지의 적 제거
            ClearEnemies();

            // 새로운 스테이지의 적 생성
            StartCoroutine(CSpawnEnemies(chapterNumber, stageNumber));


            // 플레이어 위치 초기화
            player.InitializePlayer();
        }

        private IEnumerator CSpawnEnemies(int chapterNumber, int stageNumber)
        {
            int enemyCount = (stageNumber == 3) ? 1 : 5; // 보스 스테이지는 1마리
            enemies = new Enemy[enemyCount];

            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemyObj = Instantiate(Resources.Load<GameObject>($"Prefabs/Enemy_Chapter{chapterNumber}"));
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                enemy.InitializeEnemy(player);
                enemy.OnDeath += HandleOnEnemyDeath;
                enemyObj.transform.position = new Vector2(3.49f, Random.Range(-2.59f, -0.72f));
                enemies[i] = enemy;
                yield return new WaitForSeconds(2);
            }
        }

        private void ClearEnemies()
        {
            if (enemies != null)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy != null)
                    {
                        Destroy(enemy.gameObject);
                    }
                }
            }
        }
        
        private void OnstageCleared()
        {
            GameManager.Instance.OnStageCleared();
        }

        private void HandleOnEnemyDeath()
        {
            Debug.Log("스테이지 클리어 체크");
            if (enemies == null || enemies.Length == 0)
                return;

            foreach (Enemy enemy in enemies)
            {
                if (Deathcount == enemies.Length-1)
                {
                    // 스테이지 클리어
                    Debug.Log("스테이지 클리어!");
                    Invoke("OnstageCleared", 1f);
                    
                    Deathcount = 0;
                    return;
                }

                if (enemy != null)
                {
                    Deathcount++;
                    return;
                }
                
            }
        }
    }
}
