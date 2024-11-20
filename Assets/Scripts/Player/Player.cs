using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Animator animator {  get; private set; }
    public HealthSystem healthSystem { get; private set; }
    private PlayerStateMachine stateMachine;
    public Rigidbody2D rb;
    public LayerMask targetMask;
    private SpriteRenderer spriteRenderer;
    public bool isDie = false;
    private bool isPoisoned = false;  // �� ���� ����
    public Color nomalDamageColor = Color.red;  // ������ �� ����
    public Color poisonColor = new Color(0.5f, 0f, 0.5f);
    public float blinkDuration = 0.1f;  // �����̴� �ð�

    public PlayerSaveData playerSaveData;

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
        healthSystem.player.HealthDecrease(damage);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(healthSystem.player.currentValue);
        
        healthSystem.player.HealthDecrease(damage);
        StartCoroutine(nameof(BlinknomalDamageColor));

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
    private IEnumerator BlinknomalDamageColor()
    {
       spriteRenderer.color = nomalDamageColor;
        yield return new WaitForSeconds(blinkDuration);
        spriteRenderer.color = Color.white;
    }

    public void SetSaveData(PlayerSaveData data)
    {
        //playerSaveData = data;
        //
        //// �÷��̾��� ������ ����� �����ͷ� ����
        //Data.playerData.BaseDamage = playerSaveData.BaseDamage;
        //Data.playerData.BaseAttackRate = playerSaveData.BaseAttackRate;
        //Data.playerData.BaseMaxHealth = playerSaveData.BaseMaxHealth;
        //Data.playerData.BaseAttackaDirection = playerSaveData.BaseAttackDirection;
        //Data.playerData.BaseSpeed = playerSaveData.BaseSpeed;
        //
        //// ü�� �ý��� ������Ʈ
        //healthSystem.player.SetMaxHealth(playerSaveData.BaseMaxHealth);
        //healthSystem.player.currentValue = playerSaveData.CurrentHealth;

        if (data == null)
        {
            Debug.LogError("PlayerSaveData�� null�Դϴ�.");
            return;
        }

        try
        {
            // ������ ���� ����
            playerSaveData = data;

            // ��: ü�� �� ���� ����
            Data.playerData.BaseDamage = data.BaseDamage;
            Data.playerData.BaseAttackRate = data.BaseAttackRate;
            Data.playerData.BaseMaxHealth = data.BaseMaxHealth;
            Data.playerData.BaseAttackaDirection = data.BaseAttackDirection;
            Data.playerData.BaseSpeed = data.BaseSpeed;

            healthSystem.player.SetMaxHealth(data.BaseMaxHealth);
            healthSystem.player.currentValue = data.CurrentHealth;
        }
        catch (Exception ex)
        {
            Debug.LogError($"SetSaveData �� ���� �߻�: {ex.Message}");
        }

    }
}
