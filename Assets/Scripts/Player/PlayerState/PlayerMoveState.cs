using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    Vector3 playerPosition;
    public override void Enter()
    {
        moveSpeed = 1f;     // 플레이어의 데이터에서 스피드 가져오기
        StartAnimation(stateMachine.Player.animationData.MovingParameterHash);

    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Player.animationData.MovingParameterHash);
    }

    public override void Update()
    {
    }
    public override void FixedUpdate()
    {
        PositionMove(startPosition);
    }

    public void PositionMove(Vector3 targetPosition)
    {
         playerPosition = stateMachine.Player.transform.position;
         Vector3 distance = targetPosition - playerPosition;
        Vector3 move = distance.normalized * moveSpeed * Time.deltaTime;
        if (Vector2.Distance(stateMachine.Player.transform.position, targetPosition) > 0.1f)
        {
            // 이동
            //Debug.Log("이동?");
            stateMachine.Player.rb.MovePosition(playerPosition + move);
        }
        else
        {
            stateMachine.Player.rb.velocity = Vector2.zero;
        }
                
    }
}
