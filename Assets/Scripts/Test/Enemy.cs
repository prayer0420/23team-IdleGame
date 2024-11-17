using System;
using UnityEngine;
using GameProject.Characters;
using GameProject.Levels;

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
        public Action OnDeath { get; set; }

        public void InitializeEnemy(Player targetPlayer)
        {
            HP = 30;
            Speed = 1000f;
            rb = GetComponent<Rigidbody2D>();
            player = targetPlayer;
        }
        private void Update()
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= 2f)
            {
                Attack();
            }
            else
            {
                Move();
            }
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
            player.TakeDamage(1);
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
            Debug.Log("적 사망");
            Destroy(gameObject,1f); 
            OnDeath?.Invoke();
            StageManager.Instance.DeSubScribeAction(this);
        }


    }
}
