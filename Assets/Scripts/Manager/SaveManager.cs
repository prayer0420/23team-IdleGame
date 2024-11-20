using UnityEngine;
using System.IO;
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private string SavePath => Path.Combine(Application.persistentDataPath, "SaveData.json");
    private const int INITNUMBER = 1;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("start");
        GameManager.Instance.InitializeGame();
    }

    public SaveData LoadGame()
    {
        Debug.Log("저장 경로: " + SavePath);

        if (!File.Exists(SavePath))
        {
            return null;
        }
        try
        {
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"게임 로드 실패: {e.Message}");
            return null;
        }
    }

    public void SaveGame(SaveData saveData)
    {
        try
        {
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(SavePath, json);
            Debug.Log("게임 저장");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"게임 저장 실패: {e.Message}");
        }
    }


}