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
        currentState?.Exit();       // ���� ���� ����
        currentState = state;       // ���� ������Ʈ
        currentState?.Enter();      // ���� ����
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();     // �ش� �����϶� �� �����Ӹ��� ȣ��
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();      // ������ ������ ȣ��
    }
}