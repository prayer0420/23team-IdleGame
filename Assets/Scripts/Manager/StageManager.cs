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
            Debug.Log("��������");
        }

        public void InitializeStageManager(Player player)
        {
            this.player = player;
        }
        public void StartStage(int chapterNumber, int stageNumber)
        {
            // ���� ���������� �� ����
            ClearEnemies();

            // ���ο� ���������� �� ����
            StartCoroutine(CSpawnEnemies(chapterNumber, stageNumber));


            // �÷��̾� ��ġ �ʱ�ȭ
            player.InitializePlayer();
        }

        private IEnumerator CSpawnEnemies(int chapterNumber, int stageNumber)
        {
            int enemyCount = (stageNumber == 3) ? 1 : 5; // ���� ���������� 1����
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
            Debug.Log("�������� Ŭ���� üũ");
            if (enemies == null || enemies.Length == 0)
                return;

            foreach (Enemy enemy in enemies)
            {
                if (Deathcount == enemies.Length-1)
                {
                    // �������� Ŭ����
                    Debug.Log("�������� Ŭ����!");
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
