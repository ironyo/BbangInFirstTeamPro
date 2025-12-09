using System.Collections.Generic;
using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<GameObject, Factory> _factories = new Dictionary<GameObject, Factory>();

    public void RegisterPool(GameObject prefab, int poolSize)
    {
        if (prefab == null || _factories.ContainsKey(prefab)) return;

        Factory factory = new Factory(prefab, poolSize);
        _factories.Add(prefab, factory);
    }

    public IRecycleObject GetObject (GameObject prefab)
    {
        if (_factories.TryGetValue(prefab, out Factory factory))
        {
            return factory.Get();
        }
        return null;
    }
}
