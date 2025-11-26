using UnityEngine;

public enum FoodType
{
    Meat,
    Fish,
    Salad,
    Dessert,
    Alien
}
public class CustomerMoveTruck : MonoBehaviour
{
    [Header("기본 이동 설정")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private FoodType playerWantFoodType;
    [SerializeField] private float arriveThreshold = 0.05f;

    [Header("트럭 위치 설정")]
    [SerializeField] private Transform meatPos1;
    [SerializeField] private Transform meatPos2;

    [SerializeField] private Transform fishPos1;
    [SerializeField] private Transform fishPos2;

    [SerializeField] private Transform saladPos1;
    [SerializeField] private Transform saladPos2;

    [SerializeField] private Transform dessertPos1;
    [SerializeField] private Transform dessertPos2;

    [SerializeField] private Transform alienPos1;
    [SerializeField] private Transform alienPos2;

    private Transform target;

    private void Start()
    {
        SetTargetPosition();
    }

    private void Update()
    {
        if (target == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        float dist = Vector2.Distance(transform.position, target.position);
        if (dist < arriveThreshold)
        {
            //도착 이벤트  
        }
    }

    private void SetTargetPosition()
    {
        Transform posA = null;
        Transform posB = null;

        switch (playerWantFoodType)
        {
            case FoodType.Meat:
                posA = meatPos1;
                posB = meatPos2;
                break;

            case FoodType.Fish:
                posA = fishPos1;
                posB = fishPos2;
                break;

            case FoodType.Salad:
                posA = saladPos1;
                posB = saladPos2;
                break;

            case FoodType.Dessert:
                posA = dessertPos1;
                posB = dessertPos2;
                break;

            case FoodType.Alien:
                posA = alienPos1;
                posB = alienPos2;
                break;
        }

        if (posA == null && posB == null) return;
        if (posA != null && posB == null) { target = posA; return; }
        if (posA == null && posB != null) { target = posB; return; }

        float distA = Vector2.Distance(transform.position, posA.position);
        float distB = Vector2.Distance(transform.position, posB.position);
        target = (distA < distB) ? posA : posB;
    }

    public void SetFoodType(FoodType newType)
    {
        playerWantFoodType = newType;
        SetTargetPosition();
    }
}
