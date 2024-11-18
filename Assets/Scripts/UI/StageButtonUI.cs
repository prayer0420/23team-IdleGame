using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageNumberText;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Button button;

    private int stageNumber;

    public void Initialize(int stagenumber, StageData data)
    {
        this.stageNumber = stagenumber;
        stageNumberText.text = stagenumber.ToString();

        UpdateUI(data);

        button.onClick.AddListener(() =>
        {
            GameManager.Instance.StartStage(stageNumber, true);
        });
    }

    public void UpdateUI(StageData data)
    {
        lockIcon.SetActive(!data.isUnlocked);
        button.interactable = data.isUnlocked;
    }
}
