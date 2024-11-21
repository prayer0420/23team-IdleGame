using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ObjectPool<T> where T : UnityEngine.Object
{
    private T prefab;
    private Queue<T> pool;
    private Transform parentTransform;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parentTransform = parent;
        pool = new Queue<T>();

        for (int i = 0; i < initialSize; i++)
        {
            T obj = Object.Instantiate(prefab, parentTransform);
            DeactivateInstance(obj);
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        T obj;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Object.Instantiate(prefab, parentTransform);
        }
        Debug.Log($"{obj.name} 가져오기");

        ActivateInstance(obj);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        Debug.Log($"{obj.name} 반환");
        DeactivateInstance(obj);
        pool.Enqueue(obj);
    }

    private void ActivateInstance(T obj)
    {
        if (obj is GameObject go)
        {
            go.SetActive(true);
        }
        else if (obj is Component comp)
        {
            comp.gameObject.SetActive(true);
        }
    }

    private void DeactivateInstance(T obj)
    {
        if (obj is GameObject go)
        {
            go.SetActive(false);
        }
        else if (obj is Component comp)
        {
            comp.gameObject.SetActive(false);
        }
    }
}
