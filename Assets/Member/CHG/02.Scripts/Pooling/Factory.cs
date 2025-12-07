using System.Collections.Generic;
using UnityEngine;
namespace Assets.Member.CHG._02.Scripts.Pooling
{
    public class Factory
    {
        private List<IRecycleObject> _poolObj = new List<IRecycleObject>();
        private int _defaultPoolSize;
        private GameObject _prefab;

        public Factory(GameObject prefab, int defaultPoolSize = 5)
        {
            if (prefab == null) return;
            _prefab = prefab;
            _defaultPoolSize = defaultPoolSize;
        }

        private void CreatePool()
        {
            for (int i = 0; i < _defaultPoolSize; i++)
            {
                GameObject obj = GameObject.Instantiate(_prefab);
                IRecycleObject objRecycle = obj.GetComponent<IRecycleObject>();

                objRecycle.Destroyed += Restore;
                obj.gameObject.SetActive(false);
                _poolObj.Add(objRecycle);
            }
        }

        public IRecycleObject Get()
        {
            if (_poolObj.Count == 0)
            {
                CreatePool();
            }

            int lastIndex = _poolObj.Count - 1;
            IRecycleObject obj = _poolObj[lastIndex];
            _poolObj.RemoveAt(lastIndex);
            obj.GameObject.SetActive(true);
            return obj;
        }

        public void Restore(IRecycleObject obj)
        {
            if (obj == null) return;

            obj.GameObject.SetActive(false);
            _poolObj.Add(obj);
        }
    }
}
