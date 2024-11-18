using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
        moveSpeed = 0.001f;     // 플레이어의 데이터에서 스피드 가져오기
                              //StartAnimation(stateMachine.Player.animationData.MovingParameterHash);

    }

    public override void Exit()
    {
        //StopAnimation(stateMachine.Player.animationData.MovingParameterHash);
    }

    public override void Update()
    {
        PositionMove(startPosition);

        AttackDirectionCheck();


        if (AttackDirectionCheck() == true)
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }


    }

    public void PositionMove(Vector2 targetPosition)
    {
        if (Vector2.Distance(playerTransform, startPosition) > 0.1f)
        {
            Vector2 direction = targetPosition - playerTransform;
            Vector2 moveDirection = direction.normalized * moveSpeed * Time.deltaTime;

            rb.MovePosition(moveDirection);
        }
        
    }
}
