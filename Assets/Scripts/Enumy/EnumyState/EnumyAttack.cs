using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumyAttack : EnumyBaseSt
{
    public EnumyAttack(EnumyStateMachine stateMachine) : base(stateMachine) { }

    private float lastAttackTime;
   
    float damage;
    public bool isAttacking = false;
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
        
    }

    private void OnAttack()
    {
        if (Time.time - lastAttackTime > enumyData.AttackRate)
        {
            isAttacking = true;

            lastAttackTime = Time.time;
            if(isAttacking)
            {
               
                SetTriggerAnimation(stateMachine.Enumy.animationData.AttackParameterHash);
                RaycastHit2D hit = Physics2D.Raycast(stateMachine.Enumy.transform.position, stateMachine.Enumy.transform.right * -1, enumyData.AttackDirection, stateMachine.Enumy.targetMask);

                Debug.Log(hit.collider.gameObject.name);
                hit.collider.GetComponent<TakeDamage>().TakeDamage(damage);
            }
           

            
        }
        else isAttacking = false;

    }
}
