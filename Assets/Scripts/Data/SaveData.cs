using System;

[Serializable]
public class SaveData
{
    public int currentChapter;
    public int currentStage;
    public DifficultyLevel difficulty;
    public GameProgressData progress;

    public SaveData()
    {
        currentChapter = 1;
        currentStage = 1;
        difficulty = DifficultyLevel.Normal;
        progress = new GameProgressData();
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



