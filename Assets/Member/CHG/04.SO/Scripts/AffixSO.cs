using UnityEngine;

public enum AffixType
{
    AddPower,
    AddRange,
    LowCoolTime
}

namespace Assets.Member.CHG._04.SO.Scripts
{
    [CreateAssetMenu(fileName = "AffixSO", menuName = "C_SO/AffixSO")]
    public class AffixSO : ScriptableObject
    {
        public Sprite AffixSprite;
        public int AffixPrice;

        public AffixType AffixType;
        public int Value;

    }
}