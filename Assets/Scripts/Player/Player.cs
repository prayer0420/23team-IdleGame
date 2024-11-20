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
    [field: SerializeField] public EnumySO enumy {  get; set; }

    public Animator animator {  get; private set; }
    public HealthSystem healthSystem { get; private set; }
    private PlayerStateMachine stateMachine;
    public Rigidbody2D rb;
    public LayerMask targetMask;
    private SpriteRenderer spriteRenderer;
    private Coroutine poisonCoroutine;
    private float timer = 0f;
    public bool isDie = false;
    public bool isPoisoned = false;


    public Color nomalDamageColor = Color.red;  // 데미지 시 색상
    public Color poisonColor = new Color(0.5f, 0f, 0.5f);
    public float blinkDuration = 0.1f;  // 깜박이는 시간



    private void Awake()
    {
        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();
        stateMachine = new PlayerStateMachine(this);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
      
    }
    void Start()
    {
        stateMachine.ChangeState(stateMachine.MoveState);
        healthSystem = GetComponent<HealthSystem>();
       
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
        if(poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
        }
        
        poisonCoroutine = StartCoroutine(PoisonDamage(damage));
        
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

    private IEnumerator BlinkPoisonDamageColor()
    {
        spriteRenderer.color = poisonColor;
        yield return new WaitForSeconds(blinkDuration);
        spriteRenderer.color = Color.white;
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

        if( timer < enumy.enumyData.poisonDuration)
        {
            timer += Time.deltaTime;
        }
        else if(timer >= enumy.enumyData.poisonDuration)
        {
            isPoisoned = false;
            timer = 0;
        }
        
    }
}
