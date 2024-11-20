using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enumy : MonoBehaviour, TakeDamage
{
    [field: Header("Animation")]
    [field: SerializeField] public AnimationData animationData;
    [field: SerializeField] public EnumySO Data { get; set; }

    public Animator animator { get; private set; }
    private EnumyStateMachine enumyStateMachine;
    public Rigidbody2D rb;
    public LayerMask targetMask;
    public Player targetPlayer;


    private void Awake()
    {
        animationData.Initialize();
        animator = GetComponent<Animator>();
        enumyStateMachine = new EnumyStateMachine(this);
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        enumyStateMachine.ChangeState(enumyStateMachine.EnumyMove);
       targetPlayer = GameManager.Instance.player;
    }
    private void Update()
    {
        // ������ ���� ��ġ (Enumy ��ü�� ��ġ)
        Vector2 rayStart = enumyStateMachine.Enumy.transform.position;

        // ������ ���� (Enumy ��ü�� ������ �ݴ� ����)
        Vector2 rayDirection = enumyStateMachine.Enumy.transform.right * -1;

        // Raycast �õ�
        RaycastHit2D hit = Physics2D.Raycast(rayStart, rayDirection, Data.enumyData.AttackDirection, targetMask);

        // ���̸� �׸���
        Debug.DrawRay(rayStart, rayDirection * Data.enumyData.AttackDirection, Color.red);

        // ���̰� � ��ü�� �浹������ �浹 ������ �α׷� ���
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
    }
}