using UnityEngine;
using UnityEngine.UI;

public class AttackSpeedSlider : MonoBehaviour
{
    private RectTransform rect;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    public void Init(Transform turret, float attackSpeed)
    {
        Vector3 worldPos = turret.position + new Vector3(0, 3f, 0);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        rect.position = screenPos;

        transform.parent = GameObject.Find("TurretAttackSpeedSliderCanvas").transform;
    }

    public void UpdateSlider(float attackTime)
    {
        image.fillAmount = attackTime;
    }
}
