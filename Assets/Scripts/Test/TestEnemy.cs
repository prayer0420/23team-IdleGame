using System;
using UnityEngine;

public class TestEnemy : MonoBehaviour, ICharacter
{
    public int HP { get; set; }
    public float Speed { get; set; }
    public bool IsAlive => HP > 0;
    private Rigidbody2D rb;
    private TestPlayer player;
    public Action OnDeath { get; set; }

    public void InitializeEnemy(TestPlayer targetPlayer)
    {
        HP = 30;
        Speed = 5000f;
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
        Invoke("Death", 1f);
        Destroy(gameObject,1);
    }

    public void Death()
    {
        Debug.Log("death");
        OnDeath?.Invoke();
    }
}
