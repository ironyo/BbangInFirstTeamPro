using UnityEngine;
using UnityEngine.UI;

public class AttackSpeedSlider : MonoBehaviour
{
    private RectTransform rect;
    private Image image;

    private void Awake()
    {
        image = GetComponentsInChildren<Image>()[1];
        rect = GetComponent<RectTransform>();
    }

    public void Init(Transform turret)
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
