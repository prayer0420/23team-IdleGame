using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnumyCondition : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Enumy enumy;

    public float currentValue;
    private float maxValue;

    private void Awake()
    {
        maxValue = enumy.Data.enumyData.MaxHealth;
        currentValue = maxValue;
        UpdateUI();
    }

    public void UpdateUI()
    {
        slider.value = currentValue / maxValue;
    }

    public void Init(Enumy enumy)
    {
        this.enumy = enumy;
        maxValue = enumy.Data.enumyData.MaxHealth;
        currentValue = maxValue;

        UpdateUI();
    }

    public void HealthAdd(float amount)
    {
        if (currentValue >= maxValue) return;
        currentValue += amount;
        UpdateUI();
    }
    public void HealthDecrease(float amount)
    {
        if (currentValue <= 0f) return;
        currentValue -= amount;
        UpdateUI();

    }
}
