using UnityEngine;

public class RoadMover : MonoBehaviour
{
    [SerializeField] private Transform[] _roads;
    [SerializeField] private RoadManager _manager;

    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (_manager.CurrentSpeed <= 0f) return;

        float move = _manager.CurrentSpeed * Time.deltaTime;
        float leftEdge = _cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;

        foreach (var road in _roads)
        {
            road.position += Vector3.left * move;

            var roadRenderer = road.GetComponent<SpriteRenderer>();

            const float SEAM_OVERLAP = 0.1f; // 0.01 ~ 0.03 사이 추천

            if (roadRenderer.bounds.max.x < leftEdge)
            {
                SpriteRenderer rightMost = GetRightMostRoad();

                float offset =
                    rightMost.bounds.max.x - roadRenderer.bounds.min.x;

                road.position += Vector3.right * (offset - SEAM_OVERLAP);
            }

        }
    }

    private SpriteRenderer GetRightMostRoad()
    {
        SpriteRenderer rightMost =
            _roads[0].GetComponent<SpriteRenderer>();

        foreach (var road in _roads)
        {
            var sr = road.GetComponent<SpriteRenderer>();
            if (sr.bounds.max.x > rightMost.bounds.max.x)
                rightMost = sr;
        }

        return rightMost;
    }
}
