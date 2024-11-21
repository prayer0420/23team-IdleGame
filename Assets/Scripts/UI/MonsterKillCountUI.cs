using TMPro;
using UnityEngine;

public class MonsterKillCountUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI MonsterKillCount;


    public void UpdateUI(int currentMonsterCount, int MaxMonsterCount)
    {
        MonsterKillCount.text = $"óġ�ؾ��� ���� {currentMonsterCount} / {MaxMonsterCount}";
    }
}
