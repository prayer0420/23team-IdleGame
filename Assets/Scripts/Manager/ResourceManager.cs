using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private Dictionary<string, Object> resourceCache;
    private Dictionary<string, Object[]> resourceArrayCache;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            resourceCache = new Dictionary<string, Object>();
            resourceArrayCache = new Dictionary<string, Object[]>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public T LoadResource<T>(string path) where T : Object
    {
        if (resourceCache.TryGetValue(path, out Object cachedResource))
        {
            return cachedResource as T;
        }
        else
        {
            T resource = Resources.Load<T>(path);
            if (resource != null)
            {
                resourceCache[path] = resource;
            }
            else
            {
                Debug.LogError($"경로에 리소스가 없음: {path}");
            }
            return resource;
        }
    }

    public T[] LoadAllResources<T>(string path) where T : Object
    {
        if (resourceArrayCache.TryGetValue(path, out Object[] cachedResources))
        {
            // Object[] 에서 T[]로 변환
            T[] resources = new T[cachedResources.Length];
            for (int i = 0; i < cachedResources.Length; i++)
            {
                resources[i] = cachedResources[i] as T;
            }
            return resources;
        }
        else
        {
            T[] resources = Resources.LoadAll<T>(path);
            if (resources != null && resources.Length > 0)
            {
                Object[] objects = new Object[resources.Length];
                for (int i = 0; i < resources.Length; i++)
                {
                    objects[i] = resources[i];
                }
                resourceArrayCache[path] = objects;
            }
            else
            {
                Debug.LogError($"경로에 리소스가 없음: {path}");
            }
            return resources;
        }
    }
}
