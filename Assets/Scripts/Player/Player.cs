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
    public Vector2 currentPosition;
   

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
        currentPosition = transform.position;
        stateMachine.Update();
    }

    public void TakeDamage(float damage)
    {
        
    }
}
