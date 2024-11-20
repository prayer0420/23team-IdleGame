using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    private float lastAttackTime;
    public bool isAttacking =false;
    

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

                hit.collider.GetComponent<TakeDamage>().TakeDamage(damage);
            }
           

            Debug.Log("@@@@");
        }
        else isAttacking = false;
        
    }

}
