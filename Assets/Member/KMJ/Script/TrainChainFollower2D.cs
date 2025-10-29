using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// head 다음으로 이어지는 세그먼트(객차)들이 일정 간격(linkLength)으로 부드럽게 따라오도록 만듬.
/// 세그먼트들은 Transform만 있어도 되고, Rigidbody2D가 있어도 Kinematic 권장.
/// </summary>
public class TrainChainFollower2D : MonoBehaviour
{
    [Header("References")]
    [Tooltip("기관차(헤드). 보통 CarHeadMovement2D 가 달린 오브젝트의 Transform")]
    public Transform head;

    [Tooltip("헤드를 따르는 객차들(앞에서부터 순서대로).")]
    public List<Transform> cars = new List<Transform>();

    [Header("Chain Settings")]
    [Tooltip("세그먼트 간 고정 간격(단위: 유니티 단위)")]
    public float linkLength = 1.5f;

    [Tooltip("회전 응답 속도(도/초). 값이 클수록 즉각적으로 앞칸 방향을 바라봄")]
    public float turnSpeed = 720f;

    [Tooltip("위치 보정 강도. 값이 클수록 간격 유지가 빠르게 맞춰짐")]
    public float followTightness = 20f;

    [Tooltip("세그먼트가 한 번에 움직일 수 있는 최대 거리 제한(폭주 방지)")]
    public float maxSnapPerFixed = 2.0f;

    void Reset()
    {
        // 에디터에서 추가 시 자동 검색
        if (head == null && transform.childCount > 0)
            head = transform.GetChild(0);
    }

    void FixedUpdate()
    {
        if (head == null || cars == null || cars.Count == 0) return;

        Vector2 prevPos = head.position;
        Vector2 prevDir = head.up; // CarHeadMovement2D 가 transform.up 방향으로 달리도록 설계됨

        for (int i = 0; i < cars.Count; i++)
        {
            Transform seg = cars[i];
            if (seg == null) continue;

            // 현재 세그먼트가 바라봐야 하는 방향 = 앞 세그먼트(prev) -> 현재 세그먼트(seg)
            Vector2 toPrev = (Vector2)prevPos - (Vector2)seg.position;
            Vector2 desiredDir = toPrev.sqrMagnitude > 1e-6f ? toPrev.normalized : prevDir;

            // 회전 보정 (Slerp 대용: RotateTowards)
            float targetAngle = Mathf.Atan2(desiredDir.y, desiredDir.x) * Mathf.Rad2Deg - 90f;
            float newZ = Mathf.MoveTowardsAngle(seg.eulerAngles.z, targetAngle, turnSpeed * Time.fixedDeltaTime);
            seg.rotation = Quaternion.Euler(0, 0, newZ);

            // 위치 보정: 앞 세그먼트(prevPos)에서 seg가 seg.up의 반대방향으로 linkLength 만큼 뒤에 오도록
            Vector2 idealPos = prevPos - (Vector2)seg.up * linkLength;
            Vector2 delta = idealPos - (Vector2)seg.position;

            // 과도 스냅 방지
            float maxStep = maxSnapPerFixed;
            if (delta.magnitude > maxStep)
                delta = delta.normalized * maxStep;

            seg.position += (Vector3)(delta * followTightness * Time.fixedDeltaTime);

            // 다음 루프를 위해 업데이트
            prevPos = seg.position;
            prevDir = seg.up;
        }
    }
}

