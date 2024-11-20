using UnityEngine;

public class EnumyPoisonAttack : EnumyBaseState
{
    public EnumyPoisonAttack(EnumyStateMachine stateMachine) : base(stateMachine) { }

    private float lastAttackTime;
    private float poisonAttackTime;

    float poisonDamage;
    float timer;
    public bool isAttacking;

    public override void Enter()
    {
        poisonDamage = enumyData.poisonDamage;
       
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        OnPoisonAttack();


    }

    private void OnPoisonAttack()
    {
        if (Time.time - lastAttackTime > enumyData.AttackRate)
        {
            isAttacking = true;

            lastAttackTime = Time.time;
            if (isAttacking)
            {
                SetTriggerAnimation(stateMachine.Enumy.animationData.AttackParameterHash);
                RaycastHit2D hit = Physics2D.Raycast(stateMachine.Enumy.transform.position, stateMachine.Enumy.transform.right * -1, enumyData.AttackDirection, stateMachine.Enumy.targetMask);

                hit.collider.GetComponent<TakeDamage>().ApplyPoisonDamage(enumyData.poisonDamage);

            }

        }
        else isAttacking = false;

    }
}

