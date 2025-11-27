using System.IO;
using UnityEngine;

public class RoadMove : MonoBehaviour
{
    [SerializeField] private Transform[] roads;
    [SerializeField] private float speed;

    private float roadLength;

    private void Start()
    {
        roadLength = roads[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        foreach (Transform road in roads)
        {
            road.position += Vector3.left * speed * Time.deltaTime;

            if (road.position.x <= -roadLength)
            {
                road.position += Vector3.right * roadLength * roads.Length;
            }
        }
    }
}
