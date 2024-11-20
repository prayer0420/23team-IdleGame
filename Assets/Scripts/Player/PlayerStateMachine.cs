public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

   
    public PlayerMoveState MoveState { get; }
    public PlayerAttackState AttackState { get; }
    public PlayerDieState DieState { get; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

       
        MoveState = new PlayerMoveState(this);
        AttackState = new PlayerAttackState(this);
        DieState = new PlayerDieState(this);




    }
}
