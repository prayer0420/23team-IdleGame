using Unity.VisualScripting;
public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}
public abstract class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState state)
    {
        currentState?.Exit();       // 현재 상태 제거
        currentState = state;       // 상태 업데이트
        currentState?.Enter();      // 상태 시작
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();     // 해당 상태일때 매 프레임마다 호출
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();      // 물리적 움직임 호출
    }
}