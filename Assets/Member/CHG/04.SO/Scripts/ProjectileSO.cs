using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "C_SO/ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
    public Sprite ProjectileSprite;
    public int ProjectilePrice;

    public GameObject ProjectilePrefab;
    public int PoolSize;
}
