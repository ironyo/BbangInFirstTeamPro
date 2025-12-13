using UnityEngine;
using UnityEngine.UI;

public class AttackSpeedSlider : MonoBehaviour
{
    private RectTransform rect;
    private Image image;
    private Transform target;

    private void Awake()
    {
        image = GetComponentsInChildren<Image>()[1];
        rect = GetComponent<RectTransform>();
    }

    public void Init(Transform turret)
    {
        target = turret;

        transform.SetParent(GameObject.Find("TurretAttackSpeedSliderCanvas").transform, false);
    }

    private void LateUpdate()
    {
        if (target == null)
            Destroy(gameObject);

        Vector3 worldPos = target.position + Vector3.up * 1f;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        rect.position = screenPos;
    }

    public void UpdateSlider(float attackTime)
    {
        image.fillAmount = attackTime;
    }
}
