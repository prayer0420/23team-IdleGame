using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumyMove : EnumyBaseState
{
    public EnumyMove(EnumyStateMachine stateMachine) : base(stateMachine) { }

   
    Vector3 enumyPosition;
    float enumySpeed;
    float enumyDistance;
    

    public override void Enter()
    {
        enumySpeed = enumyData.Speed = 1f; // 적 접근 속도
        enumyDistance = enumyData.AttackDirection;      // 적 사정거리
       
        StartAnimation(stateMachine.Enumy.animationData.MovingParameterHash);

    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Enumy.animationData.MovingParameterHash);

    }

    public override void FixedUpdate()
    {
        PositionMove(stateMachine.Enumy.targetPlayer.position);
    }

    public override void Update()
    {
    }

    public void PositionMove(Vector3 targetPosition)
    {
        enumyPosition = stateMachine.Enumy.transform.position;
        
        Vector3 distance = targetPosition - enumyPosition;
        Vector3 move = distance.normalized * enumySpeed * Time.deltaTime;
        if (Vector2.Distance(enumyPosition, targetPosition) > enumyDistance)
        {
            // 이동
            stateMachine.Enumy.rb.MovePosition(enumyPosition + move);

        }
        else
        {
            stateMachine.Enumy.rb.velocity = Vector3.zero;
            return;
        } 
    }
}
