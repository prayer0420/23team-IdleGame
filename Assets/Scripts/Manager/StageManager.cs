using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    private Player player;
    private Enumy[] enemies;
    private int enemyKillCount;
    private List<Enumy> activeEnemies = new List<Enumy>();
    private readonly Vector2 bossPosition = new Vector2(-5.64f , -0.62f);


    private Dictionary<string, ObjectPool<Enumy>> enemyPools = new Dictionary<string, ObjectPool<Enumy>>();

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

    public void InitializeStageManager(Player player)
    {
        this.player = player;
    }

    public void StartStage(int chapter, int stage, DifficultyLevel difficulty)
    {
        enemyKillCount = 0;
        ClearEnemies();
        StopAllCoroutines();
        StartCoroutine(SpawnEnemiesCoroutine(chapter, stage, difficulty));
    }

    private IEnumerator SpawnEnemiesCoroutine(int chapter, int stage, DifficultyLevel difficulty)
    {
        if (stage == 3) // ���� ��������
        {
            string prefabPath = GetEnemyPrefabPath(chapter, "Boss", difficulty);
            ObjectPool<Enumy> bossEnemyPool = GetOrCreateEnemyPool(prefabPath, 1);
            Enumy boss = bossEnemyPool.Get();
            boss.Init();
            boss.PrefabPath = prefabPath; // PrefabPath ����
            boss.monsterType = MonsterType.normal; // ������ �Ϲ� Ÿ��
            boss.OnDeath += HandleEnemyDeath;
            boss.transform.position = bossPosition;

            enemies = new Enumy[1];
            enemies[0] = boss;
        }
        else // �Ϲ� ��������
        {
            int normalEnemyCount = 5;
            int poisonEnemyCount = 0;

            if (chapter >= 2)
            {
                normalEnemyCount = 3;
                poisonEnemyCount = 2;
            }

            int totalEnemyCount = normalEnemyCount + poisonEnemyCount;
            enemies = new Enumy[totalEnemyCount];
            int enemyIndex = 0;

            // �Ϲ� ���� ����
            string normalPrefabPath = GetEnemyPrefabPath(chapter, "Normal", difficulty);
            ObjectPool<Enumy> normalEnemyPool = GetOrCreateEnemyPool(normalPrefabPath, normalEnemyCount);

            for (int i = 0; i < normalEnemyCount; i++)
            {
                Enumy enemy = normalEnemyPool.Get();
                enemy.Init();
                enemy.PrefabPath = normalPrefabPath;
                enemy.monsterType = MonsterType.normal;
                enemy.OnDeath += HandleEnemyDeath;
                enemy.transform.position = GetRandomSpawnPosition();

                enemies[enemyIndex++] = enemy;
                Debug.Log($"�븻 {normalEnemyCount} �� {i}���� ����");
                yield return new WaitForSeconds(2f); // �� ���� ���� ����
            }

            // �� ���� ����
            if (poisonEnemyCount > 0)
            {
                string poisonPrefabPath = GetEnemyPrefabPath(chapter, "Poison", difficulty);
                ObjectPool<Enumy> poisonEnemyPool = GetOrCreateEnemyPool(poisonPrefabPath, poisonEnemyCount);

                for (int i = 0; i < poisonEnemyCount; i++)
                {
                    Enumy enemy = poisonEnemyPool.Get();
                    enemy.Init(); 
                    enemy.PrefabPath = poisonPrefabPath; 
                    enemy.monsterType = MonsterType.poison;
                    enemy.OnDeath += HandleEnemyDeath;
                    enemy.transform.position = GetRandomSpawnPosition();

                    enemies[enemyIndex++] = enemy;

                    yield return new WaitForSeconds(2f); // �� ���� ���� ����
                }
            }
        }
    }

    private string GetEnemyPrefabPath(int chapter, string enemyType, DifficultyLevel difficulty)
    {
        string prefabPath = $"Prefabs/Enemy/Enemy_Chapter{chapter}_{enemyType}";
        if (difficulty == DifficultyLevel.Hard)
        {
            prefabPath += "_Hard";
        }
        return prefabPath;
    }

    // ObjectPool�� ���� �� ����
    private ObjectPool<Enumy> GetOrCreateEnemyPool(string prefabPath, int initialSize)
    {
        if (!enemyPools.TryGetValue(prefabPath, out ObjectPool<Enumy> enemyPool))
        {
            GameObject enemyPrefabObj = ResourceManager.Instance.LoadResource<GameObject>(prefabPath);

            Enumy enemyPrefab = enemyPrefabObj.GetComponent<Enumy>();

            enemyPool = new ObjectPool<Enumy>(enemyPrefab, initialSize);
            enemyPools[prefabPath] = enemyPool;
        }
        return enemyPool;
    }

    private void HandleEnemyDeath(Enumy enemy)
    {
        enemy.OnDeath -= HandleEnemyDeath;
        Debug.Log($"�׾���1");
        string prefabPath = enemy.PrefabPath;
        if (enemyPools.TryGetValue(prefabPath, out ObjectPool<Enumy> enemyPool))
        {
            enemyPool.ReturnToPool(enemy);
            Debug.Log($"�׾���2");
        }
        else
        {
            Debug.LogError($"�� Ǯ�� ã�� �� ����: {prefabPath}");
        }

        if (AreAllEnemiesDead())
        {
            Debug.Log("OnStageClear");
            Invoke("OnStageClear", 1.5f);
        }
    }

    public void OnStageClear()
    {
        GameManager.Instance.OnStageCleared();
    }

    private bool AreAllEnemiesDead()
    {
        enemyKillCount++;
        Debug.Log($"���� ���� {enemyKillCount} ���� óġ, óġ�ؾ��� ���� {enemies.Length}����");
        if (enemyKillCount >= enemies.Length)
        {
            enemyKillCount = 0;
            return true;
        }
        return false;
    }

    public void ClearEnemies()
    {
        if (enemies != null)
        {
            foreach (Enumy enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.OnDeath -= HandleEnemyDeath;
                    string prefabPath = enemy.PrefabPath;
                    if (enemyPools.TryGetValue(prefabPath, out ObjectPool<Enumy> enemyPool))
                    {
                        enemyPool.ReturnToPool(enemy);
                        
                    }
                    else
                    {
                        enemy.gameObject.SetActive(false);
                    }
                }
            }
        }
        Debug.Log("�� ��� ��Ȱ��ȭ");
    }

    private Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(5.64f, Random.Range(-1.034f, -0.834f));
    }
}
