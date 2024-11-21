using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public interface TakeDamage
{
    public void TakeDamage(float damage);
    public void ApplyPoisonDamage(float damage);
}
public class Player : MonoBehaviour, TakeDamage
{
    [field: Header("Animation")]
    [field: SerializeField] public AnimationData animationData;
    [field: SerializeField] public PlayerSO Data { get;  set; }
    [field: SerializeField] public EnumySO enumy { get; set; }

    public Animator animator {  get; private set; }
    public HealthSystem healthSystem { get; private set; }
    public PlayerStateMachine stateMachine;
    public Rigidbody2D rb;
    public LayerMask targetMask;
    private SpriteRenderer spriteRenderer;
    private Coroutine poisonCoroutine;
    public bool isDie = false;
    private bool isPoisoned = false;  // �� ���� ����
    public Color nomalDamageColor = Color.red;  // ������ �� ����
    public Color poisonColor = new Color(0.5f, 0f, 0.5f);
    public float blinkDuration = 0.1f;  // �����̴� �ð�

    public PlayerSaveData playerSaveData;

    public Action PlayerOnDeath;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

        playerSaveData = new PlayerSaveData(Data, Data.playerData.BaseMaxHealth);
        playerSaveData.BaseDamage = Data.playerData.BaseDamage;
        playerSaveData.BaseAttackRate = Data.playerData.BaseAttackRate;
        playerSaveData.BaseMaxHealth = Data.playerData.BaseMaxHealth;
        playerSaveData.BaseAttackDirection = Data.playerData.BaseAttackaDirection;
        playerSaveData.BaseSpeed = Data.playerData.BaseSpeed;
        playerSaveData.CurrentHealth = Data.playerData.BaseMaxHealth;


        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();
        stateMachine = new PlayerStateMachine(this);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }
    void Start()
    {
        stateMachine.ChangeState(stateMachine.MoveState);
    }

   
    void Update()
    {
        if (healthSystem.player.currentValue <= 0f) stateMachine.ChangeState(stateMachine.DieState);
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
    public void ApplyPoisonDamage(float damage)
    {
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
        }

        poisonCoroutine = StartCoroutine(PoisonDamage(damage));

    }

    public void TakeDamage(float damage)
    {
        healthSystem.player.HealthDecrease(damage);
        StartCoroutine(nameof(BlinknomalDamageColor));

    }
    public void OnDie()
    {
        StartCoroutine(nameof(WaitDieTime));
        stateMachine.AttackState.Reset();
    }


    public IEnumerator WaitDieTime()
    {
        yield return new WaitForSeconds(2.0f);

        StopAllCoroutines();
        //�÷��̾� ���� �˸�
        PlayerOnDeath?.Invoke();
    }
    private IEnumerator PoisonDamage(float damage)
    {
        isPoisoned = true;
        while (isPoisoned)
        {
            healthSystem.player.HealthDecrease(damage);
            StartCoroutine(nameof(BlinkPoisonDamageColor));
            yield return new WaitForSeconds(enumy.enumyData.poisonInterval);
        }
    }
    private IEnumerator BlinknomalDamageColor()
    {
       spriteRenderer.color = nomalDamageColor;
        yield return new WaitForSeconds(blinkDuration);
        spriteRenderer.color = Color.white;
    }

    private IEnumerator BlinkPoisonDamageColor()
    {
        spriteRenderer.color = poisonColor;
        yield return new WaitForSeconds(blinkDuration);
        spriteRenderer.color = Color.white;
    }


    public void SetSaveData(PlayerSaveData data)
    {
        playerSaveData = data;

        // �÷��̾��� ������ ����� �����ͷ� ����
        Data.playerData.BaseDamage = playerSaveData.BaseDamage;
        Data.playerData.BaseAttackRate = playerSaveData.BaseAttackRate;
        Data.playerData.BaseMaxHealth = playerSaveData.BaseMaxHealth;
        Data.playerData.BaseAttackaDirection = playerSaveData.BaseAttackDirection;
        Data.playerData.BaseSpeed = playerSaveData.BaseSpeed;

        // ü�� �ý��� ������Ʈ
        healthSystem.player.SetMaxHealth(playerSaveData.BaseMaxHealth);
        healthSystem.player.currentValue = playerSaveData.CurrentHealth;

    }

    public void Init()
    {
        // ü�� �ý��� �ʱ�ȭ
        if (healthSystem == null)
        {
            healthSystem = GetComponent<HealthSystem>();
        }
        healthSystem.player.Init();

        // ���� ���� �ʱ�ȭ
        isDie = false;

        // �ִϸ����� �ʱ�ȭ
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        animator.Rebind();
        animator.Update(0f);

        // ���� �ӽ� �ʱ�ȭ
        if (stateMachine == null)
        {
            stateMachine = new PlayerStateMachine(this);
        }
        stateMachine.ChangeState(stateMachine.MoveState);

        // ��Ÿ �ʿ��� ���� �ʱ�ȭ
        spriteRenderer.color = Color.white; // ���� �ʱ�ȭ

        // �̺�Ʈ �ڵ鷯 �ʱ�ȭ
        PlayerOnDeath = null;
    }

}
