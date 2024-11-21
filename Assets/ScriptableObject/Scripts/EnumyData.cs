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
    [field: SerializeField][field: Range(0f, 10f)] public float PoisonDuration { get; set; } = 2f;  // 독 지속 시간 (초)
    [field: SerializeField][field: Range(0f, 10f)] public float PoisonInterval { get; set; } = 0.3f;  // 독 데미지가 주어지는 간격 (초)
    [field: SerializeField][field: Range(0f, 10f)] public int PoisonDamage { get; set; } = 5;  // 독으로 입는 데미지

    [field: Header("Stun Stat")]
    [field: SerializeField][field: Range(0f, 20f)]  public float KnockbackForce { get; set; } = 10f;  // 넉백의 강도
    [field: SerializeField][field: Range(0f, 20f)] public float StunDuration { get; set; } = 2f;     // 스턴 지속 시간
    [field: SerializeField][field: Range(0f, 10f)] public int StunDamage { get; set; } = 10;  // 독으로 입는 데미지

}
