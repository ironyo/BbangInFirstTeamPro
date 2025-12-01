using UnityEngine;

public class RoadMover : MonoBehaviour
{
    [SerializeField] private Transform[] _roads;
    [SerializeField] private RoadManager _manager;

    private float _roadLength;

    private void Start()
    {
        _roadLength = _roads[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        if (_manager.CurrentSpeed <= 0f) return;

        float moveValue = _manager.CurrentSpeed * Time.deltaTime;

        foreach (var road in _roads)
        {
            road.position += Vector3.left * moveValue;

            if (road.position.x <= -_roadLength)
            {
                road.position += Vector3.right * _roadLength * _roads.Length;
            }
        }
    }
}
