using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : Object
{
    private T prefab;
    private Queue<T> pool;
    private Transform parentTransform;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parentTransform = parent;
        pool = new Queue<T>();
        //초기에 생성
        for (int i = 0; i < initialSize; i++)
        {
            T obj = Object.Instantiate(prefab, parentTransform);
            DeactivateInstance(obj);
            pool.Enqueue(obj);
        }
    }
    //풀에서 가져오기
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
        ActivateInstance(obj);
        return obj;
    }
    //풀로 반환
    public void ReturnToPool(T obj)
    {
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
