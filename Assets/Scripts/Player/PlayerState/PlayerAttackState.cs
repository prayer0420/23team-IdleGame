using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    private float lastAttackTime;
    public bool isAttacking =false;

    private Queue<Enumy> enemyQueue = new Queue<Enumy>();

    public override void Enter()
    {
        
       
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        OnAttack();
        
      
    }
    public override void FixedUpdate()
    {
        
    }
    private void OnAttack()
    {
        if (Time.time - lastAttackTime > playerData.BaseAttackRate)
        {

            isAttacking = true;

            lastAttackTime = Time.time;
            if (isAttacking) 
            {
            SetTriggerAnimation(stateMachine.Player.animationData.AttackParameterHash);
                RaycastHit2D hit = Physics2D.Raycast(stateMachine.Player.transform.position, stateMachine.Player.transform.right, playerData.BaseAttackaDirection,stateMachine.Player.targetMask);

                if (hit.collider != null)
                {
                    Enumy enumy = hit.collider.GetComponent<Enumy>();
                    if (enumy != null)
                    {
                        AddEnemy(enumy);
                        Attack();
                    }
                }
            }
            //Debug.Log("@@@@");
        }
        else isAttacking = false;
        
    }

    public void AddEnemy(Enumy enumy)
    {
        if (!enemyQueue.Contains(enumy))
        {
            enumy.OnDeath += RemoveEnemy;
            enemyQueue.Enqueue(enumy);
        }
    }

    public void RemoveEnemy(Enumy enumy)
    {
        var tempList = new List<Enumy>(enemyQueue);
        tempList.Remove(enumy);

        enemyQueue.Clear();
        foreach (var enemy in tempList)
        {
            
            enemyQueue.Enqueue(enemy);
        }
    }

    public void Attack()
    {
        if (enemyQueue.Count > 0)
        {
            var enemy = enemyQueue.Peek(); 
            if (enemy != null && !enemy.isDie)
            {
                enemy.TakeDamage(damage);
            }
            else
            {
                enemyQueue.Dequeue();
                enemy.OnDeath -= RemoveEnemy;
            }
        }
    }
}
