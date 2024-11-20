using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TakeDamage
{
    public void TakeDamage(float damage);
}
public class Player : MonoBehaviour, TakeDamage
{
    [field: Header("Animation")]
    [field: SerializeField] public AnimationData animationData;
    [field: SerializeField] public PlayerSO Data { get;  set; }

    public Animator animator {  get; private set; }
    public HealthSystem HealthSystem { get; private set; }
    public PlayerBaseState baseState {  get; set; }
    private PlayerStateMachine stateMachine;
    public Rigidbody2D rb;
    public LayerMask targetMask;
    public bool isDie = false;
    


    private void Awake()
    {
        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();
        stateMachine = new PlayerStateMachine(this);
      
    }
    void Start()
    {
        stateMachine.ChangeState(stateMachine.MoveState);
        HealthSystem = GetComponent<HealthSystem>();
       
    }

   
    void Update()
    {
        if (HealthSystem.player.currentValue <= 0f) stateMachine.ChangeState(stateMachine.DieState);
        AttackDirectionCheck();
        stateMachine.Update();
        
    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void AttackDirectionCheck()
    {
          
        RaycastHit2D hit = Physics2D.Raycast(stateMachine.Player.transform.position, stateMachine.Player.transform.right, Data.playerData.BaseAttackaDirection, targetMask);
        if (hit.collider == null)
        {
            stateMachine.ChangeState(stateMachine.MoveState);
        }
        else if(hit.collider != null && !isDie)
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
       
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(HealthSystem.player.currentValue);
        


        
        
        HealthSystem.player.HealthDecrease(damage);
        baseState.SetTriggerAnimation(stateMachine.Player.animationData.GetDamageParameterHash);

    }
    public void OnDie()
    {
        StartCoroutine(nameof(WaitDieTime));
       
    }
    

    public IEnumerator WaitDieTime()
    {
        yield return new WaitForSeconds(2.0f);

        Destroy(gameObject);

    }
}
