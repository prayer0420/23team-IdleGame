using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int currentChapter;
    public int currentStage;
    public DifficultyLevel difficulty;
    public GameProgressData progress;

    public PlayerSaveData playerSaveData;
    public InventoryData inventoryData;

    public SaveData()
    {
        currentChapter = 1;
        currentStage = 1;
        difficulty = DifficultyLevel.Normal;
        progress = new GameProgressData();
        inventoryData = new InventoryData(5); 

    }
}


[Serializable]
public class GameProgressData
{
    public ChapterData[] normalChapters;
    public ChapterData[] hardChapters;

    public GameProgressData()
    {
        normalChapters = new ChapterData[3];
        hardChapters = new ChapterData[3];

        for (int i = 0; i < 3; i++)
        {
            normalChapters[i] = new ChapterData();
            hardChapters[i] = new ChapterData();
        }

        // 첫 번째 챕터와 스테이지는 기본적으로 해금
        normalChapters[0].isUnlocked = true;
        normalChapters[0].stages[0].isUnlocked = true;
    }
}

[Serializable]
public class ChapterData
{
    public StageData[] stages;
    public bool isUnlocked;

    public ChapterData()
    {
        stages = new StageData[3]; // 각 챕터당 3개의 스테이지
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i] = new StageData();
        }
    }
}

[Serializable]
public class StageData
{
    public bool isUnlocked;
    public bool isCleared;

    public StageData()
    {
        isUnlocked = false;
        isCleared = false;
    }
}


[Serializable]
public class PlayerSaveData
{
    public float BaseDamage;
    public float BaseAttackRate;
    public float BaseMaxHealth;
    public float BaseAttackDirection;
    public float BaseSpeed;
    public float CurrentHealth;

    public PlayerSaveData(PlayerSO playerSO, float currentHealth)
    {
        BaseDamage = playerSO.playerData.BaseDamage;
        BaseAttackRate = playerSO.playerData.BaseAttackRate;
        BaseMaxHealth = playerSO.playerData.BaseMaxHealth;
        BaseAttackDirection = playerSO.playerData.BaseAttackaDirection;
        BaseSpeed = playerSO.playerData.BaseSpeed;
        CurrentHealth = currentHealth;
    }
}

[Serializable]
public class InventoryData
{
    public List<ItemSlotData> itemSlots;

    public InventoryData(int slotCount)
    {
        itemSlots = new List<ItemSlotData>(slotCount);
        for (int i = 0; i < slotCount; i++)
        {
            itemSlots.Add(new ItemSlotData());
        }
    }
}


[Serializable]
public class ItemSlotData
{
    public ItemType itemType;
    public string itemID;

    public ItemSlotData()
    {
        itemType = ItemType.None;
        itemID = null;
    }

    public ItemSlotData(ItemType itemType, string itemID)
    {
        this.itemType = itemType;
        this.itemID = itemID;
    }
}

