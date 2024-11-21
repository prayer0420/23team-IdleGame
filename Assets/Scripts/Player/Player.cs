using System;
using System.Collections;
using UnityEngine;

public interface TakeDamage
{
    public void TakeDamage(float damage);
    public void ApplyPoisonDamage(float damage);
    public void StunDamage(Vector2 damagedPosition, float damage);
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
    private bool isPoisoned = false;  // 독 상태 여부
    public bool isStunned = false;  // 스턴 상태 여부
    public Color nomalDamageColor = Color.red;  // 데미지 시 색상
    public Color poisonColor = new Color(0.5f, 0f, 0.5f);
    public float blinkDuration = 0.1f;  // 깜박이는 시간

    public PlayerSaveData playerSaveData;

    public Action PlayerOnDeath;

    private string hitSFXPath = "Audio/SFX/PlayerHit";

    private float timer = 0;
    float endTime = 0;

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
        if (isStunned || isDie) return;
          
        RaycastHit2D hit = Physics2D.Raycast(stateMachine.Player.transform.position, stateMachine.Player.transform.right, Data.playerData.BaseAttackaDirection, targetMask);
        if (hit.collider == null)
        {
            stateMachine.ChangeState(stateMachine.MoveState);
        }
        else if(hit.collider != null && !isStunned)
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

    public void StunDamage(Vector2 damagedPosition, float damage)
    {
        if (isStunned) return;
          
        healthSystem.player.HealthDecrease(damage);
        StartCoroutine(nameof(BlinknomalDamageColor));
        StartCoroutine(nameof(StunCoroutine));
    }


    public void TakeDamage(float damage)
    {
        healthSystem.player.HealthDecrease(damage);
        StartCoroutine(nameof(BlinknomalDamageColor));

        AudioManager.Instance.PlaySFX(hitSFXPath);
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
        //플레이어 죽음 알림
        PlayerOnDeath?.Invoke();

        //Destroy(gameObject);
    }
    private IEnumerator PoisonDamage(float damage)
    {
        isPoisoned = true;
        endTime = enumy.enumyData.PoisonDuration + Time.time;
        while (Time.time < endTime)
        {
            healthSystem.player.HealthDecrease(damage);
            StartCoroutine(nameof(BlinkPoisonDamageColor));

            yield return new WaitForSeconds(enumy.enumyData.PoisonInterval);
        }
        isPoisoned = false;
       
    }

    private IEnumerator StunCoroutine()
    {
        isStunned = true;
        if (isStunned)
        {
            stateMachine.AttackState.isAttacking = false;
            rb.velocity = Vector2.zero;

            // 스턴 지속 시간만큼 대기
            yield return new WaitForSeconds(enumy.enumyData.StunDuration);

            // 스턴 끝
            isStunned = false;
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

        // 플레이어의 스탯을 저장된 데이터로 설정
        Data.playerData.BaseDamage = playerSaveData.BaseDamage;
        Data.playerData.BaseAttackRate = playerSaveData.BaseAttackRate;
        Data.playerData.BaseMaxHealth = playerSaveData.BaseMaxHealth;
        Data.playerData.BaseAttackaDirection = playerSaveData.BaseAttackDirection;
        Data.playerData.BaseSpeed = playerSaveData.BaseSpeed;

        // 체력 시스템 업데이트
        healthSystem.player.SetMaxHealth(playerSaveData.BaseMaxHealth);
        healthSystem.player.currentValue = playerSaveData.CurrentHealth;

    }

    public void Init()
    {
        if (healthSystem == null)
        {
            healthSystem = GetComponent<HealthSystem>();
        }
        healthSystem.player.Init();

        isDie = false;

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        animator.Rebind();
        animator.Update(0f);

        if (stateMachine == null)
        {
            stateMachine = new PlayerStateMachine(this);
        }
        stateMachine.ChangeState(stateMachine.MoveState);

        stateMachine.AttackState.Reset();

        spriteRenderer.color = Color.white; 

        PlayerOnDeath = null;
    }

}
