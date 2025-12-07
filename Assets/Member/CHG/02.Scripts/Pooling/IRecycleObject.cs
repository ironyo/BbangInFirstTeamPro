using System;
using System.Collections;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Pooling
{
    public interface IRecycleObject 
    {

        public Action<IRecycleObject> Destroyed { get; set; }

        public GameObject GameObject { get;}
    }
}