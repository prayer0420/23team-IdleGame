using System;
using UnityEngine;
using GameProject.Characters;
using GameProject.Managers;
using System.Collections.Generic;

namespace GameProject.Characters
{
    public class Player : MonoBehaviour, ICharacter
    {
        public int HP { get; set; }
        public float Speed { get; set; }
        public bool IsAlive => HP > 0;
        private Rigidbody2D rb;
        private BoxCollider2D collider2d;
        private List<Enemy> enemyList = new List<Enemy>();
        private Dictionary<Enemy, int> enemyIndexMap = new Dictionary<Enemy, int>();

        // �÷��̾� ��ġ �� ũ�� ����
        private Vector2 startPosition = new Vector2(-1.5f, -2.11f);

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            InitializePlayer();
            collider2d  = GetComponent<BoxCollider2D>();
        }

        public void InitializePlayer()
        {
            HP = 10000;
            Speed = 200f;

            // �ʱ� ��ġ ����
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
            foreach (Enemy enemy in enemyList)
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(30);
                    Debug.Log("�÷��̾ ���� �����߽��ϴ�.");
                }
            }
        }

        public void AddEnemy(Enemy enemy)
        {
            if (!enemyIndexMap.ContainsKey(enemy))
            {
                enemyList.Add(enemy);
                enemyIndexMap[enemy] = enemyList.Count - 1;
            }
        }

        public void RemoveEnemy(Enemy enemy)
        {
            if (enemyIndexMap.TryGetValue(enemy, out int index))
            {
                enemyList.RemoveAt(index);
                enemyIndexMap.Remove(enemy);
                // �ε��� ������
                for (int i = index; i < enemyList.Count; i++)
                {
                    enemyIndexMap[enemyList[i]] = i;
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
            GameManager.Instance.OnPlayerDeath();
            Debug.Log("�÷��̾� ���");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null && !enemyIndexMap.ContainsKey(enemy))
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
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null && enemyIndexMap.ContainsKey(enemy))
                {
                    RemoveEnemy(enemy);
                }
            }
        }


    }
}
