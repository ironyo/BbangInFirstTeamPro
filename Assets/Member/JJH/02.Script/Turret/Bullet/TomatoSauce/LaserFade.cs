using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using UnityEngine;

public class LaserFade : MonoBehaviour, IRecycleObject
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

    public Action<IRecycleObject> Destroyed { get; set; }

    public GameObject GameObject => gameObject;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        material = sprite.material;
    }

    private void OnEnable()
    {
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();

        if (material == null && sprite != null)
            material = sprite.material;

        timer = 0f;
        state = FadeState.Expand;
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
                        Destroyed?.Invoke(this);
                }
                break;
        }
    }
}
