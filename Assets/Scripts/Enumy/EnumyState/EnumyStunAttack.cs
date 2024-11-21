using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumyStunAttack : EnumyBaseState
{
    public EnumyStunAttack(EnumyStateMachine stateMachine) : base(stateMachine)
    {
       stateMachine.Enumy.currentType = MonsterType.Stun;
    }
    
    private float lastAttackTime;
    public bool isAttacking;
    Vector2 damagedPosition = new Vector2(-2.78f, 0);
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
       
        StunAttack();
    }

    public void StunAttack()
    {
        if (Time.time - lastAttackTime > enumyData.AttackRate)
        {
            isAttacking = true;

            lastAttackTime = Time.time;
            if (isAttacking)
            {
                SetTriggerAnimation(stateMachine.Enumy.animationData.AttackParameterHash);
                RaycastHit2D hit = Physics2D.Raycast(stateMachine.Enumy.transform.position, stateMachine.Enumy.transform.right * -1, enumyData.AttackDirection, stateMachine.Enumy.targetMask);

                hit.collider.GetComponent<TakeDamage>().StunDamage(damagedPosition, enumyData.StunDamage);
                hit.collider.GetComponent<Rigidbody2D>().AddForce(damagedPosition * enumyData.KnockbackForce, ForceMode2D.Impulse);
                

            }

        }
        else isAttacking = false;

    }
}

