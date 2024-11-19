using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enumy", menuName = "Character/Enumy")]
public class EnumySO : ScriptableObject
{
    [field: SerializeField] public EnumyData enumyData { get; private set; }
}
