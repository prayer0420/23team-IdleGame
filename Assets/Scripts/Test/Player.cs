using System;
using UnityEngine;
using GameProject.Characters;
using GameProject.Managers;

namespace GameProject.Characters
{
    public class Player : MonoBehaviour, ICharacter
    {
        public int HP { get; set; }
        public float Speed { get; set; }
        public bool IsAlive => HP > 0;
        private Rigidbody2D rb;

        // �÷��̾� ��ġ �� ũ�� ����
        private Vector2 startPosition = new Vector2(100, 640);
        private Vector2 size = new Vector2(100, 100);

        private void Start()
        {
            HP = 100;
            Speed = 200f;
            rb = GetComponent<Rigidbody2D>();

            // �ʱ� ��ġ ����
            transform.position = startPosition;
            transform.localScale = size;
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
            // ���� ���� ���� ������ ������
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 50);
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.TakeDamage(20);
                Debug.Log("�÷��̾ ������ ����");
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
        }

        private void Update()
        {
            // ���� ���� �Ÿ� ���� ������ ����, �ƴϸ� �̵�
            Enemy nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null && Vector2.Distance(transform.position, nearestEnemy.transform.position) <= 50f)
            {
                Attack();
            }
            else
            {
                //Move();
            }
        }

        private Enemy FindNearestEnemy()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Enemy nearest = null;
            float minDistance = float.MaxValue;
            foreach (Enemy enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = enemy;
                }
            }
            return nearest;
        }
    }
}
