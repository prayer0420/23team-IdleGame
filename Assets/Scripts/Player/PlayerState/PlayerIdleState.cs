using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    

    public override void Enter()
    {
        StartAnimation(stateMachine.Player.animationData.IdleParameterHash);
    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Player.animationData.IdleParameterHash);
    }

    public override void Update()
    {
    }

   
    
}
