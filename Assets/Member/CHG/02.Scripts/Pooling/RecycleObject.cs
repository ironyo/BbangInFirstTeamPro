using System;
using System.Collections;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Pooling
{
    public class RecycleObject : MonoBehaviour
    {
        protected bool IsActiveated = false;

        protected Vector3 TargetPos;

        public Action<RecycleObject> Destroyed;
        public Action<RecycleObject> OutOfScreen;

        public virtual void Activate(Vector2 pos)
        {
            IsActiveated = true;
            transform.position = pos;
        }

        
    }
}