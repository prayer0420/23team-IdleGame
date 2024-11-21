using System;
using System.Collections;
using UnityEngine;

public enum MonsterType
{
    normal,
    poison,
    Stun
}
public class Enumy : MonoBehaviour, TakeDamage
{
    [field: Header("Animation")]
    [field: SerializeField] public AnimationData animationData;
    [field: SerializeField] public EnumySO Data { get; set; }
    [field: SerializeField] public MonsterType monsterType {  get; set; }

    public Animator animator { get; private set; }
    public HealthSystem healthSystem { get; set; }

    private EnumyStateMachine enumyStateMachine;
    public Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    public LayerMask targetMask;
    public MonsterType currentType;
    public Player targetPlayer;
    public bool isDie = false;
    private float fadeDuration = 2.0f;
    public Color normalDamageColor = Color.red;  // ������ �� ����
    public float blinkDuration = 0.1f;  // �����̴� �ð�

    public Action<Enumy> OnDeath { get; set; }
    public string PrefabPath { get; set; }

    private void Awake()
    {
        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();
        enumyStateMachine = new EnumyStateMachine(this);
        rbSprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        enumyStateMachine.ChangeState(enumyStateMachine.EnumyMove);
        healthSystem = GetComponent<HealthSystem>();
        targetPlayer = GameManager.Instance.player;
    }
    private void Update()
    {
        if (healthSystem.enumy.currentValue <= 0f) enumyStateMachine.ChangeState(enumyStateMachine.EnumyDie);
        AttackDirectionCheck();
        enumyStateMachine.Update();
    }
    private void FixedUpdate()
    {
        enumyStateMachine.FixedUpdate();
    }
    public void Init()
    {
        // ü�� �ý��� �ʱ�ȭ
        if (healthSystem == null)
        {
            healthSystem = GetComponent<HealthSystem>();
        }
        healthSystem.enumy.Init(this);

        // ���� ���� �ʱ�ȭ
        isDie = false;

        // �ִϸ����� �ʱ�ȭ
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        animator.Rebind();
        animator.Update(0f);

        // ��Ÿ �ʿ��� ���� �ʱ�ȭ
        gameObject.layer = LayerMask.NameToLayer("Enumy"); // �� ���̾� ����
        rbSprite.color = Color.white; // ���� �ʱ�ȭ

        // ���� �ӽ� �ʱ�ȭ
        if (enumyStateMachine == null)
        {
            enumyStateMachine = new EnumyStateMachine(this);
        }
        enumyStateMachine.ChangeState(enumyStateMachine.EnumyMove);
    }


    public void AttackDirectionCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(enumyStateMachine.Enumy.transform.position, enumyStateMachine.Enumy.transform.right * -1, Data.enumyData.AttackDirection, targetMask);
        if (hit.collider == null && targetPlayer.isStunned)
        {
            enumyStateMachine.ChangeState(enumyStateMachine.EnumyMove);
        }
        else if (hit.collider != null)
        {
            //if(monsterType == currentType)
            //{

            //}

            if (monsterType == MonsterType.normal) enumyStateMachine.ChangeState(enumyStateMachine.EnumyAttack);
            else if (monsterType == MonsterType.poison) enumyStateMachine.ChangeState(enumyStateMachine.EnumyPoisonAttack);
            else if (monsterType == MonsterType.Stun) enumyStateMachine.ChangeState(enumyStateMachine.EnumyStunAttack);

        }
    }

    
    public void ApplyPoisonDamage(float damage)
    {
    }
    public void StunDamage(Vector2 damagedPosition, float damage)
    {
    }
    public void TakeDamage(float damage)
    {
        healthSystem.enumy.HealthDecrease(damage);
        Debug.Log("�¾Ҵ�");
        StartCoroutine(nameof(BlinkDamageColor));
    }

    public void OnDie()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        StartCoroutine(nameof(FadeOutAndDie));
        //OnDeath?.Invoke(this);
        Debug.Log("�׾��� �˸�");
    }

    private IEnumerator FadeOutAndDie()
    {
        float elapsedTime = 0f;  // ��� �ð�

        Color startColor = rbSprite.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / fadeDuration);  // ���İ��� ���� 0���� ����
            rbSprite.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rbSprite.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        OnDeath?.Invoke(this);
        Debug.Log("�׾��� �˸�");

        //Destroy(gameObject);
    }

    private IEnumerator BlinkDamageColor()
    {
        rbSprite.color = normalDamageColor;
        yield return new WaitForSeconds(blinkDuration);
        rbSprite.color = Color.white;
    }

}