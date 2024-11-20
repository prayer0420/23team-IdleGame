using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enumy : MonoBehaviour, TakeDamage
{
    [field: Header("Animation")]
    [field: SerializeField] public AnimationData animationData;
    [field: SerializeField] public EnumySO Data { get; set; }

    public Animator animator { get; private set; }
    public HealthSystem healthSystem { get;  set; }
    private EnumyStateMachine enumyStateMachine;
    public Rigidbody2D rb;
    public LayerMask targetMask;
    public Transform targetPlayer;
    

    private void Awake()
    {
        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();
         enumyStateMachine = new EnumyStateMachine(this);
    }
    private void Start()
    {
        enumyStateMachine.ChangeState(enumyStateMachine.EnumyMove);
        healthSystem = GetComponent<HealthSystem>();
    }
    private void Update()
    {
        // 레이의 시작 위치 (Enumy 객체의 위치)
        Vector2 rayStart = enumyStateMachine.Enumy.transform.position;

        // 레이의 방향 (Enumy 객체의 오른쪽 반대 방향)
        Vector2 rayDirection = enumyStateMachine.Enumy.transform.right * -1;

        // Raycast 시도
        RaycastHit2D hit = Physics2D.Raycast(rayStart, rayDirection, Data.enumyData.AttackDirection, targetMask);

        // 레이를 그리기
        Debug.DrawRay(rayStart, rayDirection * Data.enumyData.AttackDirection, Color.red);

        // 레이가 어떤 객체와 충돌했으면 충돌 정보를 로그로 출력
        if (hit.collider != null)
        {
            Debug.Log("Hit " + hit.collider.name);
        }
        AttackDirectionCheck();
        enumyStateMachine.Update();

        
    }
    private void FixedUpdate()
    {
        enumyStateMachine.FixedUpdate();
    }

    public void AttackDirectionCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(enumyStateMachine.Enumy.transform.position, enumyStateMachine.Enumy.transform.right * -1, Data.enumyData.AttackDirection, targetMask);
        if (hit.collider == null)
        {
            enumyStateMachine.ChangeState(enumyStateMachine.EnumyMove);
        }
        else
        {
            enumyStateMachine.ChangeState(enumyStateMachine.EnumyAttack);
        }
    }

    public void TakeDamage(float damage)
    {
        healthSystem.enumy.HealthDecrease(damage);
    }
}