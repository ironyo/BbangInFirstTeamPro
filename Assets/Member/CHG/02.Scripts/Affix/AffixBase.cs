using System.Collections;
using Assets.Member.CHG._04.SO.Scripts;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Affix
{
    public class AffixBase : MonoBehaviour
    {
        private AffixType _affixType;
        private float _value;

        public void Init(AffixSO affixData)
        {
            _affixType = affixData.AffixType;
            _value = affixData.Value;
        }


    }
}