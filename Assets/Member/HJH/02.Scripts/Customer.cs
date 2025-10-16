using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    [Header("고객 정보")]
    public GuestType guestType;
    public string customerName;
    public Color typeColor;
    public TextMeshPro nameText;

    [Header("이동 관련")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private Transform targetPoint;

    private void OnEnable()
    {
        FindClosestPoint();
    }

    private void Update()
    {
        if (targetPoint == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );
    }

    // 가장 가까운 스폰포인트 탐색 메소드
    private void FindClosestPoint()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning($"{name} : spawnPoints 리스트가 비어있습니다.");
            return;
        }

        float closestDist = Mathf.Infinity;
        Transform closest = null;

        foreach (Transform point in spawnPoints)
        {
            if (point == null) continue;
            float dist = Vector2.Distance(transform.position, point.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = point;
            }
        }

        targetPoint = closest;
    }

    // 손님 정보
    public void Init(GuestType type, string name)
    {
        guestType = type;
        customerName = name;

        switch (guestType)
        {
            case GuestType.None:
                break;
            case GuestType.customer:
                typeColor = Color.black;
                break;
            case GuestType.ObnoxiousCustomer:
                typeColor = Color.red;
                break;
            case GuestType.Rich:
                typeColor = Color.yellow;
                break;
            case GuestType.Celebrity:
                typeColor = Color.white;
                break;
            case GuestType.Alien:
                typeColor = Color.green;
                break;
        }

        if (nameText != null)
        {
            nameText.text = customerName;
            nameText.color = typeColor;
        }
    }

    // 스폰포인트 설정 메소드
    public void SetSpawnPoints(List<Transform> newPoints)
    {
        spawnPoints = newPoints;
        FindClosestPoint();
    }

    // 스폰포인트 추가 가능 메소드
    public void AddSpawnPoint(Transform newPoint)
    {
        if (newPoint == null) return;
        spawnPoints.Add(newPoint);
        FindClosestPoint();
    }
}
