using System.Collections.Generic;
using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class BulletPoolManager : MonoSingleton<BulletPoolManager>
{
    private Dictionary<GameObject, Factory> _pools = new Dictionary<GameObject, Factory>();
    

    public IRecycleObject Get(GameObject prefab)
    {
        if (!_pools.ContainsKey(prefab))
        {
            _pools[prefab] = new Factory(prefab, 20);
        }

        return _pools[prefab].Get();
    }
}
