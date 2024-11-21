public class EnumyDie : EnumyBaseState
{
    public EnumyDie(EnumyStateMachine stateMachine) : base(stateMachine) { }

    
    public override void Enter()
    {
        stateMachine.Enumy.isDie = true;
        stateMachine.Enumy.OnDie();

    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        stateMachine.Enumy.OnDie(); 
    }
}
