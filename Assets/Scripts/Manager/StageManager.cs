using UnityEngine;
using System.Collections;

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

    //�������� ���ۿ� ���� �� ����
    public void StartStage(int chapter, int stage, DifficultyLevel difficulty)
    {
        enemyKillCount = 0;
        StopAllCoroutines();
        //�������� ���� �� �� �� �ʱ�ȭ
        ClearEnemies();
        //�� ����(é��, ��������, ���̵��� ����)
        StartCoroutine(SpawnEnemies(chapter, stage, difficulty));
        //TODO: Player�޷�����
    }

    //�� ����(�ڷ�ƾ, 2�� ����)
    private IEnumerator SpawnEnemies(int chapter, int stage, DifficultyLevel difficulty)
    {
        //�������� 3������ ������ 1������ ����
        int enemyCount = (stage == 3) ? 1 : 5;
        
        //������ enmies ����Ʈ�� �����Ͽ� ����
        enemies = new Enumy[enemyCount];

        string prefabPath = $"Prefabs/Enemy/Enemy_Chapter{chapter}";
        if (difficulty == DifficultyLevel.Hard)
        {
            //�ϵ����� ���ʹ� �̸� �ڿ� _Hard���̱�
            prefabPath += "_Hard";
        }

        //���ҽ� �Ŵ������� �� ����
        GameObject enemyPrefab = ResourceManager.Instance.LoadResource<GameObject>(prefabPath);
        //�������� ������ ��
        if (enemyPrefab == null)
            yield break;
        
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyObj = Instantiate(enemyPrefab);
            Enumy enemy = enemyObj.GetComponent<Enumy>();
            //enemy.InitializeEnemy(player);
            enemy.OnDeath += (enemy) => HandleEnemyDeath(enemy); //������ �̺�Ʈ �߻� ����
            enemyObj.transform.position = GetRandomSpawnPosition();
            enemies[i] = enemy;
            yield return new WaitForSeconds(2f);
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
        Debug.Log($"���� ���� {enemyKillCount} ���� óġ, óġ�ؾ��� ���� {enemies.Length}����");
        if(enemyKillCount == enemies.Length)
        {
            enemyKillCount = 0;
            return true;
        }
        return false;
    }

    //�������� ���� �� �� �� �ʱ�ȭ
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
