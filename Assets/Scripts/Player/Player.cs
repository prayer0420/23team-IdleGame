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
    private PlayerStateMachine stateMachine;
    public Rigidbody2D rb;
    public LayerMask targetMask;
    


    private void Awake()
    {
        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();
        stateMachine = new PlayerStateMachine(this);
      
    }
    void Start()
    {
        stateMachine.ChangeState(stateMachine.MoveState);
       
    }

   
    void Update()
    {
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
            Debug.Log("AAA");
        }
        else
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }

    public void TakeDamage(float damage)
    {
        
    }
}
