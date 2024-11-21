using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [field: SerializeField] public PlayerCondition player {  get; set; }
    [field: SerializeField] public EnumyCondition enumy {  get; set; }
}
