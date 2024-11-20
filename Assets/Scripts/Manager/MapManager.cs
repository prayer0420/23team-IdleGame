using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MapManager : MonoBehaviour
{
    private GameObject background;
    [SerializeField] private Image fade;
    private GameObject currentMap;
    private GameObject nextMap;
    public bool IsFade = false;

    public static MapManager Instance { get; private set; }

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

    public void ChangeMap(int chapterNumber, Action callback, bool isFade)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutAndIn(chapterNumber, callback, isFade));
    }

    private void LoadMap(int chapterNumber)
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
        }

        string mapPath = $"Prefabs/Background/Background_Chapter{chapterNumber}";

        if (GameManager.Instance.CurrentDifficulty== DifficultyLevel.Hard)
        {
            //하드모드의 몬스터는 이름 뒤에 _Hard붙이기
            mapPath += "_Hard";
        }
        GameObject backgroundPrefab = ResourceManager.Instance.LoadResource<GameObject>(mapPath);
        if (backgroundPrefab != null)
        {
            currentMap = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity);
        }
    }


    private IEnumerator FadeOutAndIn(int newChapterNumber, Action callback, bool isFade)
    {

        if (isFade)
        {
            yield return StartCoroutine(Fade(0, 1));
            LoadMap(newChapterNumber);
            yield return StartCoroutine(Fade(1, 0));
        }
        else
        {
            LoadMap(newChapterNumber);
        }
        callback();
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float duration = 2f;
        float elapsed = 0f;
        Color color = fade.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            fade.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }
}
