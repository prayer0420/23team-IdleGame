using System;
using UnityEngine;

[Serializable]
public class AnimationData 
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string movingParameterName = "Moving";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string getDamageParameterName = "GetDamage";
    [SerializeField] private string dieParameterName = "Die";

    public int IdleParameterHash {  get; private set; }
    public int MovingParameterHash { get; private set ; }
    public int AttackParameterHash { get; private set;}
    public int GetDamageParameterHash { get; private set; }
    public int DieParameterHash {  get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        MovingParameterHash = Animator.StringToHash(movingParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        GetDamageParameterHash = Animator.StringToHash(getDamageParameterName);
        DieParameterHash = Animator.StringToHash(dieParameterName);
    }
}
