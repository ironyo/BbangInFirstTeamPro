using UnityEngine;

public abstract class IncreaseSpeed : MonoBehaviour
{
    protected float bulletSpeed { get; set; }
    protected float increaseSpeedTime { get; set; } = 2f;
    protected float maxSpeed { get; set; } = 80f;
    protected float timer { get; set; }

    protected float IncreaseSpeedInTime()
    {
        timer += Time.deltaTime;

        //시간에 따라 처음에 천천히 갔다가 빨리 속도 증가
        float t = Mathf.Clamp01(timer / increaseSpeedTime);
        float ease = Mathf.Pow(t, 4f);
        bulletSpeed = Mathf.Lerp(bulletSpeed, maxSpeed, ease);
        return bulletSpeed;
    }
}
