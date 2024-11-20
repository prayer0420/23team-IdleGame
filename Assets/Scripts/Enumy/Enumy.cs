using System;
using System.Collections;
using UnityEngine;

public class Enumy : MonoBehaviour, TakeDamage
{
    [field: Header("Animation")]
    [field: SerializeField] public AnimationData animationData;
    [field: SerializeField] public EnumySO Data { get; set; }

    public Animator animator { get; private set; }
    public HealthSystem healthSystem { get; set; }

    private EnumyStateMachine enumyStateMachine;
    public Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    public LayerMask targetMask;
    public Player targetPlayer;
    public bool isDie = false;
    private float fadeDuration = 2.0f;
    public Color nomalDamageColor = Color.red;  // ������ �� ����
    public float blinkDuration = 0.1f;  // �����̴� �ð�

    public Action<Enumy> OnDeath { get; set; }

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

    public void AttackDirectionCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(enumyStateMachine.Enumy.transform.position, enumyStateMachine.Enumy.transform.right * -1, Data.enumyData.AttackDirection, targetMask);
        if (hit.collider == null)
        {
            enumyStateMachine.ChangeState(enumyStateMachine.EnumyMove);
        }
        else if (hit.collider != null && !isDie)
        {
            enumyStateMachine.ChangeState(enumyStateMachine.EnumyAttack);
        }
    }

    public void ApplyPoisonDamage(float damage)
    {

    }
    public void TakeDamage(float damage)
    {
        healthSystem.enumy.HealthDecrease(damage);

        StartCoroutine(nameof(BlinkDamageColor));
    }

    public void OnDie()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        StartCoroutine(nameof(FadeOutAndDie));
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
        Destroy(gameObject);
    }

    private IEnumerator BlinkDamageColor()
    {
        rbSprite.color = nomalDamageColor;
        yield return new WaitForSeconds(blinkDuration);
        rbSprite.color = Color.white;
    }

}