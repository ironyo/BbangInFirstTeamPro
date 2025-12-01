using System.Collections.Generic;
using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class Factory
{
    private List<RecycleObject> _poolObj = new List<RecycleObject>();
    private int _defaultPoolSize;
    private RecycleObject _prefab;

    public Factory(RecycleObject prefab, int defaultPoolSize = 5)
    {
        if (prefab == null) return;
        _prefab = prefab;
        _defaultPoolSize = defaultPoolSize;
    }

    private void CreatePool()
    {
        for (int i=0; i< _defaultPoolSize; i++)
        {
            RecycleObject obj = GameObject.Instantiate(_prefab) as RecycleObject;
            obj.gameObject.SetActive(false);
            _poolObj.Add(obj);
        }
    }

    private RecycleObject Get()
    {
        if (_poolObj.Count == 0)
        {
            CreatePool();
        }

        int lastIndex = _poolObj.Count - 1;
        RecycleObject obj = _poolObj[lastIndex];
        _poolObj.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Restore(RecycleObject obj)
    {
        if (obj == null) return;

        obj.gameObject.SetActive(false);
        _poolObj.Add(obj);
    }
}
