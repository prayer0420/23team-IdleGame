using System;
using UnityEngine;
using GameProject.Characters;

namespace GameProject.Characters
{
    /// <summary>
    /// 적 캐릭터 클래스입니다.
    /// </summary>
    public class Enemy : MonoBehaviour, ICharacter
    {
        public int HP { get; set; }
        public float Speed { get; set; }
        public bool IsAlive => HP > 0;
        private Rigidbody2D rb;
        private Player player;

        private void Start()
        {
            HP = 50;
            Speed = 150f; // 150 픽셀/초
            rb = GetComponent<Rigidbody2D>();
            player = FindObjectOfType<Player>();
        }

        public void Move()
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * Speed * Time.deltaTime;
        }

        public void Idle()
        {
            rb.velocity = Vector2.zero;
        }

        public void Attack()
        {
            player.TakeDamage(10);
            Debug.Log("적이 플레이어에게 공격");
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
            Destroy(gameObject, 1f); // 1초 후 오브젝트 제거
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= 50f)
            {
                Attack();
            }
            else
            {
                Move();
            }
        }
    }
}
