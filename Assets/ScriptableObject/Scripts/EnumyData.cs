using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnumyData
{
    [field: SerializeField][field: Range(0f, 100f)] public float Damage { get; set; } 
    [field: SerializeField][field: Range(10f, 0f)] public float AttackRate { get; set; }
    [field: SerializeField][field: Range(0f, 10f)] public float AttackDirection { get; set; }

    [field: SerializeField][field: Range(0f, 300f)] public float MaxHealth { get; set; } 
    [field: SerializeField][field: Range(0f, 10f)] public float Speed { get; set; }

    [field: SerializeField][field: Range(0f, 10f)] public float poisonDuration = 2f;  // �� ���� �ð� (��)
    [field: SerializeField][field: Range(0f, 10f)] public float poisonInterval = 0.3f;  // �� �������� �־����� ���� (��)
    [field: SerializeField][field: Range(0f, 10f)] public int poisonDamage = 5;  // ������ �Դ� ������
    
}
