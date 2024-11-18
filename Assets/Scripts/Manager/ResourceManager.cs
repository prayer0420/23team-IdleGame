using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    //��θ� Ű��, �������� ������ �ϴ� ��ųʸ�
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
        //��ųʸ��� �̹� �ִٸ�
        if (resourceCache.ContainsKey(path))
        {
            return resourceCache[path] as T;
        }
        //���ٸ� ���� ������ֱ�
        else
        {
            T resource = Resources.Load<T>(path);
            if (resource != null)
            {
                resourceCache[path] = resource;
            }
            else
            {
                Debug.LogError($"���ҽ��� ��ο� ����: {path}");
            }
            return resource;
        }
    }
}
