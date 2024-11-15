using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData  
{
    [field: SerializeField][field: Range(1f, 100f)] public float BaseDamage { get;  set; } = 5f;
    [field: SerializeField][field: Range(10f, 1f)] public float BaseAttackRate { get;  set; } = 3f;
    [field: SerializeField][field: Range(1f, 300f)] public float BaseMaxHealth { get;  set; } = 100f;
}
