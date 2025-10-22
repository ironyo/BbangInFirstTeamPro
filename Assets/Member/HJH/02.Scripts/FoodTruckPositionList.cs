using System.Collections.Generic;
using UnityEngine;

public class FoodTruckPositionList : MonoBehaviour
{
    [SerializeField] private List<Transform> truckPositions = new List<Transform>();

    public List<Transform> TruckPositions => truckPositions;

    // 트럭 위치 추가 메서드
    public void AddTruckPosition(Transform newPos)
    {
        if (newPos == null) return;
        truckPositions.Add(newPos);
    }

    // 리스트 전체 설정
    public void SetTruckPositions(List<Transform> newList)
    {
        if (newList == null) return;
        truckPositions = newList;
    }
}
