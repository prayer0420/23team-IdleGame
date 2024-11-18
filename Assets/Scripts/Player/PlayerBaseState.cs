using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerData playerData;
    protected float moveSpeed;
    protected Rigidbody2D rb;
    protected Vector2 playerTransform;
    protected Vector2 startPosition = new Vector2(-1.5f, 0f);       // ������ġ
    protected float damage;
    protected float attackDirection = 0.1f;




    protected PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        playerData = stateMachine.Player.Data.playerData;
        moveSpeed = playerData.BaseSpeed;
        rb = stateMachine.Player.rb;
        playerTransform = stateMachine.Player.currentPosition;
        damage = playerData.BaseDamage;
    }


    public abstract void Enter();   // ���°� ���� �Ǿ��� �� �����ϴ� �ż���
    public abstract void Exit();    // ���°� ���� �Ǹ� ȣ�� �Ǵ� �ż���
    public abstract void Update();  // �� ������ ���� ȣ�� �Ǵ� �ż���
   

    public void StartAnimation(int animationHash)
    {
        //stateMachine.Player.animator.SetBool(animationHash, true);
    }

    public void StopAnimation(int animationHash)
    {
        //stateMachine.Player.animator.SetBool(animationHash, false);
    }

    public bool AttackDirectionCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(stateMachine.Player.transform.position, stateMachine.Player.transform.right, attackDirection);
        if (hit.collider == null) return false;
        return true;
    }
}
