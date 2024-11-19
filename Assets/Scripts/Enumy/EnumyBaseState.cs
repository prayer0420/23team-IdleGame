using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnumyBaseSt : IState
{
    protected EnumyStateMachine stateMachine;
    protected readonly EnumyData enumyData;

    

    protected EnumyBaseSt(EnumyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        enumyData = stateMachine.Enumy.Data.enumyData;
       
    }

    public abstract void Enter();   // 상태가 시작 되었을 때 시작하는 매서드
    public abstract void Exit();    // 상태가 변경 되면 호출 되는 매서드
    public abstract void Update();  // 매 프레임 마다 호출 되는 매서드
    public abstract void FixedUpdate();

    public void StartAnimation(int animationHash)
    {
        stateMachine.Enumy.animator.SetBool(animationHash, true);
    }

    public void StopAnimation(int animationHash)
    {
        stateMachine.Enumy.animator.SetBool(animationHash, false);
    }

    public void SetTriggerAnimation(int animationHash)
    {
        stateMachine.Enumy.animator.SetTrigger(animationHash);
    }
}
