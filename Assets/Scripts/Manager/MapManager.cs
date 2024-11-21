using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Pool;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }
    private GameObject background;
    [SerializeField] private Image fade;
    private GameObject nextMap;
    public bool IsFade = false;

    private GameObject currentMap;

    private ObjectPool<GameObject> currentMapPool;
    private Dictionary<string, ObjectPool<GameObject>> mapPools = new Dictionary<string, ObjectPool<GameObject>>();


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
        StartCoroutine(ChangeMapCoroutine(chapterNumber, callback, isFade));
    }


    private IEnumerator ChangeMapCoroutine(int chapterNumber, Action callback, bool isFade)
    {
        if (isFade)
        {
            yield return StartCoroutine(Fade(0, 1));
        }

        LoadMap(chapterNumber);

        if (isFade)
        {
            yield return StartCoroutine(Fade(1, 0));
        }

        callback?.Invoke();
    }

    private void LoadMap(int chapterNumber)
    {
        // 현재 맵을 풀에 반환
        if (currentMap != null && currentMapPool != null)
        {
            currentMapPool.ReturnToPool(currentMap);
            currentMap = null;
            currentMapPool = null;
        }

        string mapPath = GetMapPath(chapterNumber);

        //리소스매니저, 오브젝트풀을 활용해 맵 생성
        if (!mapPools.TryGetValue(mapPath, out ObjectPool<GameObject> mapPool))
        {
            GameObject mapPrefab = ResourceManager.Instance.LoadResource<GameObject>(mapPath);
            if (mapPrefab == null)
            {
                return;
            }

            mapPool = new ObjectPool<GameObject>(mapPrefab, 1, transform);
            mapPools[mapPath] = mapPool;
        }

        currentMapPool = mapPool;
        currentMap = currentMapPool.Get();
    }


    private string GetMapPath(int chapterNumber)
    {
        string mapPath = $"Prefabs/Background/Background_Chapter{chapterNumber}";

        if (GameManager.Instance.CurrentDifficulty == DifficultyLevel.Hard)
        {
            mapPath += "_Hard";
        }

        return mapPath;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float duration = 1.5f;
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
