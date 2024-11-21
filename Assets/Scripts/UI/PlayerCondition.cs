using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCondition : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Player player;

    public float currentValue;
    public float maxValue;

    private void Awake()
    {
        maxValue = player.Data.playerData.BaseMaxHealth;
    }
    private void Start()
    {
        Init();
    }

    public void UpdateUI()
    {
        slider.value = currentValue / maxValue;
    }

    public void HealthAdd(float amount)
    {
        if(currentValue >= maxValue) return;
        currentValue += amount;
        UpdateUI();
    }
    public void HealthDecrease(float amount)
    {
        if(currentValue <= 0f) return;
        currentValue -= amount;
        UpdateUI();
    }
    public void SetMaxHealth(float newMaxHealth)
    {
        maxValue = newMaxHealth;
        if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }
        UpdateUI();
    }

    public void Init()
    {
        currentValue = maxValue;
        UpdateUI();
    }
}
