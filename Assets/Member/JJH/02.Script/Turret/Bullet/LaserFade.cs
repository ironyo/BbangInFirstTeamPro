using UnityEngine;

public class LaserFade : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Material material;

    [SerializeField] private float startDelay = 0.2f;
    [SerializeField] private float fadeTime = 1.5f;

    private float timer = 0f;
    private bool isFading = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        material = sprite.material;

        material.SetFloat("HalfHeight", 0.5f);
    }

    private void OnEnable()
    {
        timer = 0f;
        isFading = false;
    }

    private void Update()
    {
        if (!isFading)
        {
            timer += Time.deltaTime;
            if (timer >= startDelay)
            {
                isFading = true;
                timer = 0f;
            }
            return;
        }

        timer += Time.deltaTime;

        float halfHeight = Mathf.Lerp(0.5f, 0f, timer / fadeTime);
        material.SetFloat("HalfHeight", halfHeight);

        if (halfHeight <= 0f)
            Destroy(gameObject);
    }
}
