using UnityEngine;

[CreateAssetMenu(fileName = "IngredientSO", menuName = "JJH_SO/IngredientSO")]
public class IngredientSO : ScriptableObject
{
    public string foodName;
    public FoodGroupType foodGroup;
    public FoodTasteType foodTaste;
    public FoodTextureType foodTextureType;
    public FoodRarityType foodRarityType;
}

public enum FoodGroupType //����ᱺ
{
    Vegetablse, //��ä��
    Meat, //����
    Fish, //���
    Fruit, //���Ϸ�
    MSG //��ŷ�
}

public enum FoodTasteType //��
{
    Salty, //§��
    Spicy, //�ſ��
    Sweet, //�ܸ�
    Sour, //�Ÿ�
    Bitter, //����
    Nutty //����Ѹ�
}

public enum FoodTextureType //�İ�
{
    Crispy, //�ٻ�
    Cruncky, //�ƻ�
    Chewy, //�̱�
    Tough, //����
    Soft, //�ε巯��
    Hard //������
}

public enum FoodRarityType
{
    Nomal, //�Ϲ�
    Rare, //���
    Epic, //����
    Legendary //����
}