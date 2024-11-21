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
            return resource;
        }
    }
    public T[] LoadAllResources<T>() where T : Object
    {
        string cacheKey = typeof(T).FullName;
        if (resourceArrayCache.TryGetValue(cacheKey, out Object[] cachedResources))
        {
            T[] resources = new T[cachedResources.Length];
            for (int i = 0; i < cachedResources.Length; i++)
            {
                resources[i] = cachedResources[i] as T;
            }
            return resources;
        }
        else
        {
            T[] resources = Resources.LoadAll<T>(""); 
            if (resources != null && resources.Length > 0)
            {
                Object[] objects = new Object[resources.Length];
                for (int i = 0; i < resources.Length; i++)
                {
                    objects[i] = resources[i];
                }
                resourceArrayCache[cacheKey] = objects;
            }
            return resources;
        }
    }
}
