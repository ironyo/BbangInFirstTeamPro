using UnityEngine;

[RequireComponent(typeof(Customer))]
public class CustomerMoveTruck : MonoBehaviour
{
    [Header("이동 관련")]
    [SerializeField] private float moveSpeed = 3f;

    private FoodTruckPositionList positionList;
    private Transform targetTruck;

    private void Start()
    {
        positionList = FindFirstObjectByType<FoodTruckPositionList>();

        if (positionList == null)
        {
            return;
        }

        FindClosestTruck();
    }

    private void Update()
    {
        if (targetTruck == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetTruck.position,
            moveSpeed * Time.deltaTime
        );
    }

    // 가장 가까운 푸드 트럭 입구 찾기 메서드
    private void FindClosestTruck()
    {
        float closestDist = Mathf.Infinity;
        Transform closest = null;

        foreach (Transform truck in positionList.TruckPositions)
        {
            if (truck == null) continue;

            float dist = Vector2.Distance(transform.position, truck.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = truck;
            }
        }

        targetTruck = closest;
    }


    // 외부에서 다시 트럭 리스트가 갱신될 경우 갱신용
    public void RefreshTarget()
    {
        FindClosestTruck();
    }
}
