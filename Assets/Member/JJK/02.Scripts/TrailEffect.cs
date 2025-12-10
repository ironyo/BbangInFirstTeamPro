using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    [SerializeField] private GameObject trailPrefab;
    [SerializeField] float spawnInterval = 0.05f;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            _timer = 0f;
            var obj = Instantiate(trailPrefab, transform.position, transform.rotation);
        }
    }
}
