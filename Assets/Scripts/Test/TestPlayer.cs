using System;
using UnityEngine;
using System.Collections.Generic;

public class TestPlayer : MonoBehaviour, ICharacter
{
    public int HP { get; set; }
    public float Speed { get; set; }
    public bool IsAlive => HP > 0;
    private Rigidbody2D rb;
    private BoxCollider2D collider2d;
    private List<TestEnemy> enemyList = new List<TestEnemy>();
    private Dictionary<TestEnemy, int> enemyIndexDic = new Dictionary<TestEnemy, int>();

    private Vector2 startPosition = new Vector2(-1.5f, -2.11f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        InitializePlayer();
        collider2d = GetComponent<BoxCollider2D>();
    }

    public void InitializePlayer()
    {
        HP = 10000;
        Speed = 200f;

        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
    }
    public void Move()
    {
        rb.velocity = new Vector2(Speed * Time.deltaTime, 0);
    }

    public void Idle()
    {
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        foreach (TestEnemy enemy in enemyList)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(30);
            }
        }
    }

    public void AddEnemy(TestEnemy enemy)
    {
        if (!enemyIndexDic.ContainsKey(enemy))
        {
            enemyList.Add(enemy);
            enemyIndexDic[enemy] = enemyList.Count - 1;
        }
    }

    public void RemoveEnemy(TestEnemy enemy)
    {
        if (enemyIndexDic.TryGetValue(enemy, out int index))
        {
            enemyList.RemoveAt(index);
            enemyIndexDic.Remove(enemy);
            for (int i = index; i < enemyList.Count; i++)
            {
                enemyIndexDic[enemyList[i]] = i;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //GameManager.Instance.OnPlayerDeath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TestEnemy enemy = collision.GetComponent<TestEnemy>();
            if (enemy != null && !enemyIndexDic.ContainsKey(enemy))
            {
                AddEnemy(enemy);
                Attack();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TestEnemy enemy = collision.GetComponent<TestEnemy>();
            if (enemy != null && enemyIndexDic.ContainsKey(enemy))
            {
                RemoveEnemy(enemy);
            }
        }
    }


}
