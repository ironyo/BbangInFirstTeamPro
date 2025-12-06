using System;
using System.Collections.Generic;
using UnityEngine;

public enum StoreEnum
{
    Repair, Grocery
}

public class StoreManager_TJ : MonoSingleton<StoreManager_TJ>
{
    private Dictionary<StoreEnum, Store> StoreDic = new Dictionary<StoreEnum, Store>();

    private Store _currentEnterStore = null;

    public void Register(Store store)
    {
        StoreDic[store.StoreType] = store;
    }

    public void Enter(StoreEnum type)
    {
        if (_currentEnterStore != null) return;

        _currentEnterStore = StoreDic[type];
        _currentEnterStore.Enter();
    }

    public void Exit()
    {
        _currentEnterStore = null;
    }
}
