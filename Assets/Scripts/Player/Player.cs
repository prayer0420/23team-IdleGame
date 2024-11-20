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
    private bool isPoisoned = false;  // 독 상태 여부
    public Color nomalDamageColor = Color.red;  // 데미지 시 색상
    public Color poisonColor = new Color(0.5f, 0f, 0.5f);
    public float blinkDuration = 0.1f;  // 깜박이는 시간

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
        //// 플레이어의 스탯을 저장된 데이터로 설정
        //Data.playerData.BaseDamage = playerSaveData.BaseDamage;
        //Data.playerData.BaseAttackRate = playerSaveData.BaseAttackRate;
        //Data.playerData.BaseMaxHealth = playerSaveData.BaseMaxHealth;
        //Data.playerData.BaseAttackaDirection = playerSaveData.BaseAttackDirection;
        //Data.playerData.BaseSpeed = playerSaveData.BaseSpeed;
        //
        //// 체력 시스템 업데이트
        //healthSystem.player.SetMaxHealth(playerSaveData.BaseMaxHealth);
        //healthSystem.player.currentValue = playerSaveData.CurrentHealth;

        if (data == null)
        {
            Debug.LogError("PlayerSaveData가 null입니다.");
            return;
        }

        try
        {
            // 데이터 적용 로직
            playerSaveData = data;

            // 예: 체력 및 스탯 설정
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
            Debug.LogError($"SetSaveData 중 오류 발생: {ex.Message}");
        }

    }
}
