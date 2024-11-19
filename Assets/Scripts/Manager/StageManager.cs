using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    private TestPlayer player;
    private TestEnemy[] enemies;
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

    public void InitializeStageManager(TestPlayer player)
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

    //적 생성(코루틴, 2초 마다)
    private IEnumerator SpawnEnemies(int chapter, int stage, DifficultyLevel difficulty)
    {
        //스테이지 3에서는 보스맵 1마리만 출현
        int enemyCount = (stage == 3) ? 1 : 5;
        
        //적들을 enmies 리스트에 저장하여 관리
        enemies = new TestEnemy[enemyCount];

        string prefabPath = $"Prefabs/Enemy_Chapter{chapter}";
        if (difficulty == DifficultyLevel.Hard)
        {
            //하드모드의 몬스터는 이름 뒤에 _Hard붙이기
            prefabPath += "_Hard";
        }

        //리소스 매니저에서 적 생성
        GameObject enemyPrefab = ResourceManager.Instance.LoadResource<GameObject>(prefabPath);
        //프리팹이 없으면 빽
        if (enemyPrefab == null)
            yield break;
        
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyObj = Instantiate(enemyPrefab);
            TestEnemy enemy = enemyObj.GetComponent<TestEnemy>();
            enemy.InitializeEnemy(player);
            enemy.OnDeath += () => HandleEnemyDeath(enemy); //죽을때 이벤트 발생 구독
            enemyObj.transform.position = GetRandomSpawnPosition();
            enemies[i] = enemy;
            yield return new WaitForSeconds(2f);
        }
    }


    private void HandleEnemyDeath(TestEnemy enemy)
    {
        if (AreAllEnemiesDead())
        {
            Debug.Log("OnStageClear");
            Invoke("OnStageClear",2f);
        }
    }

    public void OnStageClear()
    {
        GameManager.Instance.OnStageCleared();
    }

    private bool AreAllEnemiesDead()
    {
        enemyKillCount++;
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
            foreach (TestEnemy enemy in enemies)
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
        return new Vector2(3.49f, Random.Range(-2.59f, -0.72f));
    }
}
