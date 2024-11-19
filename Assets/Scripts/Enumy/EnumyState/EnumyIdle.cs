using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumyIdle : EnumyBaseSt
{
    public EnumyIdle(EnumyStateMachine stateMachine) : base(stateMachine) { }
    

    public override void Enter()
    {
      StartAnimation(stateMachine.Enumy.animationData.IdleParameterHash);
    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Enumy.animationData.IdleParameterHash);
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
    }
}
