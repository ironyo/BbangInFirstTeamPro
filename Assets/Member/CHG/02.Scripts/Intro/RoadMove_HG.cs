using UnityEngine;

public class RoadMove_HG : MonoBehaviour
{
    [SerializeField] private Transform[] _roads;

    private float _roadLength;
    Vector3 leftPos;

    private void Start()
    {
        _roadLength = _roads[0].GetComponent<SpriteRenderer>().bounds.size.x;

        leftPos = Camera.main.ScreenToWorldPoint(
        new Vector3(0f, Screen.height * 0.5f, Camera.main.nearClipPlane)
        );
    }

    private void Update()
    {
        float moveValue = 10 * Time.deltaTime;

        foreach (var road in _roads)
        {
            road.position += Vector3.left * moveValue;

            if (road.position.x <= leftPos.x - _roadLength / 2)
            {
                road.position += Vector3.right * _roadLength * _roads.Length;
            }
        }
    }
}
