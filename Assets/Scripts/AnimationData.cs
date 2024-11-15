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

    public int idleParameterHash {  get; private set; }
    public int movingParameterHash { get; private set ; }
    public int attackParameterHash { get; private set;}

    public void Initialize()
    {
        idleParameterHash = Animator.StringToHash(idleParameterName);
        movingParameterHash = Animator.StringToHash(movingParameterName);
        attackParameterHash = Animator.StringToHash(attackParameterName);
    }
}
