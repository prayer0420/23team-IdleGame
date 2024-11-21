using TMPro;
using UnityEngine;

public class MonsterKillCountUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI MonsterKillCount;


    public void UpdateUI(int currentMonsterCount, int MaxMonsterCount)
    {
        MonsterKillCount.text = $"처치해야할 몬스터 {currentMonsterCount} / {MaxMonsterCount}";
    }
}
