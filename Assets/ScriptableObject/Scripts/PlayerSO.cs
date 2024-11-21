using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Character/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerData playerData {  get; private set; }
}
