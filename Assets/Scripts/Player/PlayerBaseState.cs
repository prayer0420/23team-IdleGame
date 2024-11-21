using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerData playerData;
    protected float moveSpeed;
    protected float damage;
    protected Vector3 startPosition = new Vector3(-1.5f, 0f, 0f);       // ������ġ
  

    public abstract void Enter();   // ���°� ���� �Ǿ��� �� �����ϴ� �ż���
    public abstract void Exit();    // ���°� ���� �Ǹ� ȣ�� �Ǵ� �ż���
    public abstract void Update();  // �� ������ ���� ȣ�� �Ǵ� �ż���
    public abstract void FixedUpdate();
    
    protected PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        playerData = stateMachine.Player.Data.playerData;
        moveSpeed = playerData.BaseSpeed;
        damage = playerData.BaseDamage;
    }
   

    public void StartAnimation(int animationHash)
    {
        stateMachine.Player.animator.SetBool(animationHash, true);
    }

    public void StopAnimation(int animationHash)
    {
        stateMachine.Player.animator.SetBool(animationHash, false);
    }

    public void SetTriggerAnimation(int animationHash)
    {
        stateMachine.Player.animator.SetTrigger(animationHash);
    }

    public void ResetTriggerAnimation(int animationHash)
    {
        stateMachine.Player.animator.ResetTrigger(animationHash);
    }

}
