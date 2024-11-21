using UnityEngine;

public abstract class EnumyBaseState : IState
{
    protected EnumyStateMachine stateMachine;
    protected readonly EnumyData enumyData;

    protected Vector2 enumyStartPosition = new Vector2(-2.96f, 0f);

    protected EnumyBaseState(EnumyStateMachine stateMachine)
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
