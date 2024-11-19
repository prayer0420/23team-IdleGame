using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumyAttack : EnumyBaseSt
{
    public EnumyAttack(EnumyStateMachine stateMachine) : base(stateMachine) { }

    private float lastAttackTime;
    public bool isAttacking = false;
    float damage;
    public override void Enter()
    {
       damage = enumyData.Damage;
        Debug.Log("!!!");
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        OnAttack();
        if (isAttacking)
        {
            SetTriggerAnimation(stateMachine.Enumy.animationData.AttackParameterHash);
        }
    }

    private void OnAttack()
    {
        if (Time.time - lastAttackTime > enumyData.AttackRate)
        {

            lastAttackTime = Time.time;
            RaycastHit2D hit = Physics2D.Raycast(stateMachine.Enumy.transform.position, stateMachine.Enumy.transform.right * -1, enumyData.AttackDirection);
            isAttacking = true;
            hit.collider.GetComponent<TakeDamage>().TakeDamage(damage);

            
        }
        else isAttacking = false;

    }
}
