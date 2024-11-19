using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumyStateMachine : StateMachine
{
    public Enumy Enumy { get;}

    public EnumyIdle EnumyIdle { get;}
    public EnumyMove EnumyMove { get;}
    public EnumyAttack EnumyAttack {  get;}
    
    public EnumyStateMachine(Enumy enumy)
    {
        this.Enumy = enumy;
        
        EnumyIdle = new EnumyIdle(this);
        EnumyMove = new EnumyMove(this);
        EnumyAttack = new EnumyAttack(this);
    }
}
