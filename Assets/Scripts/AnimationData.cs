using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationData 
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string movingParameterName = "Moving";
    [SerializeField] private string attackParameterName = "Attack";

    public int IdleParameterHash {  get; private set; }
    public int MovingParameterHash { get; private set ; }
    public int AttackParameterHash { get; private set;}

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        MovingParameterHash = Animator.StringToHash(movingParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
    }
}
