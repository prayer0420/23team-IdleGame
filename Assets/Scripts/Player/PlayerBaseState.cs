using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerData playerData;
    protected float moveSpeed;
    protected float damage;
    protected Vector3 startPosition = new Vector3(-1.5f, 0f, 0f);       // 시작위치
  

    public abstract void Enter();   // 상태가 시작 되었을 때 시작하는 매서드
    public abstract void Exit();    // 상태가 변경 되면 호출 되는 매서드
    public abstract void Update();  // 매 프레임 마다 호출 되는 매서드
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
