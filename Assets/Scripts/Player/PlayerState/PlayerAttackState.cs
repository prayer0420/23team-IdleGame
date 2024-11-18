using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    private float lastAttackTime;
    

    public override void Enter()
    {
        //StartAnimation(stateMachine.Player.animationData.AttackParameterHash);
    }

    public override void Exit()
    {
        //StopAnimation(stateMachine.Player.animationData.AttackParameterHash);
    }

    public override void Update()
    {
        OnAttack();
        if (AttackDirectionCheck() == false) stateMachine.ChangeState(stateMachine.MoveState);
    }

    private void OnAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(stateMachine.Player.transform.position, stateMachine.Player.transform.right, attackDirection);
        if (Time.time - lastAttackTime > playerData.BaseAttackRate)
        {
            lastAttackTime = Time.time;
            //hit.collider.GetComponent<TakeDamage>().TakeDamage(damage);
        }
        
    }
}
