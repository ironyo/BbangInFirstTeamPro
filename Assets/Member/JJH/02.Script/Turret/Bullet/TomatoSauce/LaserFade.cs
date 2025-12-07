using UnityEngine;

public class LaserFade : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Material material;

    [SerializeField] private float startDelay = 0.2f;
    [SerializeField] private float expandTime = 0.3f;
    [SerializeField] private float fadeTime = 1.5f;

    private float timer = 0f;

    private enum FadeState
    {
        Expand,
        Delay,
        Fade
    }
    private FadeState state = FadeState.Expand;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        material = sprite.material;

        material.SetFloat("HalfHeight", 0f);
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        switch (state)
        {
            case FadeState.Expand:
                {
                    float t = timer / expandTime;
                    float half = Mathf.Lerp(0f, 0.5f, t);
                    material.SetFloat("HalfHeight", half);

                    if (t >= 1f)
                    {
                        state = FadeState.Delay;
                        timer = 0f;
                    }
                }
                break;
            case FadeState.Delay:
                {
                    if (timer >= startDelay)
                    {
                        state = FadeState.Fade;
                        timer = 0f;
                    }
                }
                break;
            case FadeState.Fade:
                {
                    float t = timer / fadeTime;
                    float half = Mathf.Lerp(0.5f, 0f, t);
                    material.SetFloat("HalfHeight", half);

                    if (t >= 1f)
                        Destroy(gameObject);
                }
                break;
        }
    }
}
