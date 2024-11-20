using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData  
{
    [field: SerializeField][field: Range(0f, 100f)] public float BaseDamage { get;  set; } 
    [field: SerializeField][field: Range(10f, 0f)] public float BaseAttackRate { get; set; } 
    [field: SerializeField][field: Range(0f, 300f)] public float BaseMaxHealth { get; set; } 
    [field: SerializeField][field: Range(0f, 300f)] public float BaseAttackaDirection { get; set; } 
    [field: SerializeField][field: Range(0f, 10f)] public float BaseSpeed { get; set; } 
}
