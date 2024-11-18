using Unity.VisualScripting;
public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
   
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

  

    public void Update()
    {
        currentState?.Update();     // �ش� �����϶� �� �����Ӹ��� ȣ��
    }

  
}