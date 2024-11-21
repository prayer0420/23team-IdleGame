using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerBaseState
{
    public float waitDieTime;
    public PlayerDieState(PlayerStateMachine stateMachine) : base(stateMachine) { }
   

    public override void Enter()
    {
        SetTriggerAnimation(stateMachine.Player.animationData.DieParameterHash);
        stateMachine.Player.isDie = true;
        waitDieTime = 2f;
    }

    public override void Exit()
    {
        SetTriggerAnimation(stateMachine.Player.animationData.MovingParameterHash);
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        
        stateMachine.Player.OnDie();
        
    }
    
}
