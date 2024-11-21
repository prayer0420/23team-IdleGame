
public class EnumyStateMachine : StateMachine
{
    public Enumy Enumy { get;}

    public EnumyMove EnumyMove { get;}
    public EnumyAttack EnumyAttack { get;}
    public EnumyPoisonAttack EnumyPoisonAttack { get; }
    public EnumyStunAttack EnumyStunAttack { get; }
    public EnumyDie EnumyDie { get;}

    public EnumyStateMachine(Enumy enumy)
    {
        this.Enumy = enumy;
        
        EnumyMove = new EnumyMove(this);
        EnumyAttack = new EnumyAttack(this);
        EnumyPoisonAttack = new EnumyPoisonAttack(this);
        EnumyStunAttack = new EnumyStunAttack(this);
        EnumyDie = new EnumyDie(this);
    }
}
