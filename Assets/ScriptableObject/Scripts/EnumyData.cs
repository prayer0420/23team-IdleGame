using System;
using UnityEngine;


[Serializable]
public class EnumyData
{
    [field: Header("Base Stat")]
    [field: SerializeField][field: Range(0f, 100f)] public float Damage { get; set; } 
    [field: SerializeField][field: Range(10f, 0f)] public float AttackRate { get; set; }
    [field: SerializeField][field: Range(0f, 10f)] public float AttackDirection { get; set; }
    [field: SerializeField][field: Range(0f, 300f)] public float MaxHealth { get; set; } 
    [field: SerializeField][field: Range(0f, 10f)] public float Speed { get; set; }

    [field: Header("Poison Stat")]
    [field: SerializeField][field: Range(0f, 10f)] public float PoisonDuration { get; set; } = 2f;  // �� ���� �ð� (��)
    [field: SerializeField][field: Range(0f, 10f)] public float PoisonInterval { get; set; } = 0.3f;  // �� �������� �־����� ���� (��)
    [field: SerializeField][field: Range(0f, 10f)] public int PoisonDamage { get; set; } = 5;  // ������ �Դ� ������

    [field: Header("Stun Stat")]
    [field: SerializeField][field: Range(0f, 20f)]  public float KnockbackForce { get; set; } = 10f;  // �˹��� ����
    [field: SerializeField][field: Range(0f, 20f)] public float StunDuration { get; set; } = 2f;     // ���� ���� �ð�
    [field: SerializeField][field: Range(0f, 10f)] public int StunDamage { get; set; } = 10;  // ������ �Դ� ������

}
