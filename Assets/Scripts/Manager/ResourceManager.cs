using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    //경로를 키로, 프리팹을 벨류로 하는 딕셔너리
    private Dictionary<string, Object> resourceCache;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            resourceCache = new Dictionary<string, Object>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public T LoadResource<T>(string path) where T : Object
    {
        //딕셔너리에 이미 있다면
        if (resourceCache.ContainsKey(path))
        {
            return resourceCache[path] as T;
        }
        //없다면 새로 만들어주기
        else
        {
            T resource = Resources.Load<T>(path);
            if (resource != null)
            {
                resourceCache[path] = resource;
            }
            else
            {
                Debug.LogError($"리소스가 경로에 없음: {path}");
            }
            return resource;
        }
    }
}
