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

    public abstract void Enter();   // ���°� ���� �Ǿ��� �� �����ϴ� �ż���
    public abstract void Exit();    // ���°� ���� �Ǹ� ȣ�� �Ǵ� �ż���
    public abstract void Update();  // �� ������ ���� ȣ�� �Ǵ� �ż���
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
