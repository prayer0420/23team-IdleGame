using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    private Player player;
    private Enumy[] enemies;
    private int enemyKillCount;

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

    //스테이지 시작에 따른 적 생성
    public void StartStage(int chapter, int stage, DifficultyLevel difficulty)
    {
        enemyKillCount = 0;
        StopAllCoroutines();
        //스테이지 시작 될 때 적 초기화
        ClearEnemies();
        //적 생성(챕터, 스테이지, 난이도에 따른)
        StartCoroutine(SpawnEnemies(chapter, stage, difficulty));
        //TODO: Player달려오기
    }

    private IEnumerator SpawnEnemies(int chapter, int stage, DifficultyLevel difficulty)
    {
        //보스 스테이지
        if (stage == 3)
        {
            string prefabPath = $"Prefabs/Enemy/Enemy_Chapter{chapter}_Boss";
            if (difficulty == DifficultyLevel.Hard)
            {
                prefabPath += "_Hard";
            }

            // 보스 몬스터 로드
            GameObject bossPrefab = ResourceManager.Instance.LoadResource<GameObject>(prefabPath);
            if (bossPrefab == null)
                yield break;

            GameObject bossObj = Instantiate(bossPrefab);
            Enumy boss = bossObj.GetComponent<Enumy>();
            boss.monsterType = MonsterType.normal; //보스는 노말타입
            boss.OnDeath += (enemy) => HandleEnemyDeath(enemy);
            bossObj.transform.position = GetRandomSpawnPosition();
            enemies = new Enumy[1];
            enemies[0] = boss;
        }
        else //일반적인 스테이지
        {
            int normalEnemyCount = 5;
            int poisonEnemyCount = 0;
            
            if(chapter>=2)
            {
                normalEnemyCount = 3;
                poisonEnemyCount = 2;
            }

            List<Enumy> enemyList = new List<Enumy>();

            //일반몬스터
            string normalPrefabPath = $"Prefabs/Enemy/Enemy_Chapter{chapter}_Normal";
            if (difficulty == DifficultyLevel.Hard)
            {
                normalPrefabPath += "_Hard";
            }

            GameObject normalEnemyPrefab = ResourceManager.Instance.LoadResource<GameObject>(normalPrefabPath);
            if (normalEnemyPrefab == null)
                yield break;

            for (int i = 0; i < normalEnemyCount; i++)
            {
                GameObject enemyObj = Instantiate(normalEnemyPrefab);
                Enumy enemy = enemyObj.GetComponent<Enumy>();
                enemy.monsterType = MonsterType.normal;
                enemy.OnDeath += (e) => HandleEnemyDeath(e);
                enemyObj.transform.position = GetRandomSpawnPosition();
                enemyList.Add(enemy);
                yield return new WaitForSeconds(2f);
            }

            //독 몬스터도 생성
            if (poisonEnemyCount > 0)
            {
                string poisonPrefabPath = $"Prefabs/Enemy/Enemy_Chapter{chapter}_Poison";
                if (difficulty == DifficultyLevel.Hard)
                {
                    poisonPrefabPath += "_Hard";
                }

                GameObject poisonEnemyPrefab = ResourceManager.Instance.LoadResource<GameObject>(poisonPrefabPath);
                if (poisonEnemyPrefab == null)
                    yield break;

                for (int i = 0; i < poisonEnemyCount; i++)
                {
                    GameObject enemyObj = Instantiate(poisonEnemyPrefab);
                    Enumy enemy = enemyObj.GetComponent<Enumy>();
                    enemy.monsterType = MonsterType.poison;
                    enemy.OnDeath += (e) => HandleEnemyDeath(e);
                    enemyObj.transform.position = GetRandomSpawnPosition();
                    enemyList.Add(enemy);
                    yield return new WaitForSeconds(2f);
                }
            }

            enemies = enemyList.ToArray();
        }
    }


    private void HandleEnemyDeath(Enumy enemy)
    {
        if (AreAllEnemiesDead())
        {
            Debug.Log("OnStageClear");
            Invoke("OnStageClear",1.5f);
        }
    }

    public void OnStageClear()
    {
        GameManager.Instance.OnStageCleared();
    }

    private bool AreAllEnemiesDead()
    {
        enemyKillCount++;
        Debug.Log($"현재 몬스터 {enemyKillCount} 마리 처치, 처치해야할 몬스터 {enemies.Length}마리");
        if(enemyKillCount == enemies.Length)
        {
            enemyKillCount = 0;
            return true;
        }
        return false;
    }

    //스테이지 시작 될 때 적 초기화
    public void ClearEnemies()
    {
        if (enemies != null)
        {
            foreach (Enumy enemy in enemies)
            {
                if (enemy != null)
                {
                    Destroy(enemy.gameObject);
                }
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(5.64f, Random.Range(-1.22f, -0.876f));
        //return new Vector2(5.64f, -1.3f);
    }
}
