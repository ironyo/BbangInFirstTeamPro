using UnityEngine;
using TMPro;

public class AutoBGImgSizeCtrl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private float padding = 20f;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (targetText == null)
            targetText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateBackgroundSize()
    {
        if (targetText == null) return;

        // 텍스트의 실제 렌더링 영역 계산
        targetText.ForceMeshUpdate();
        float textHeight = targetText.textBounds.size.y;

        // 배경 크기 조절
        Vector2 size = rectTransform.sizeDelta;
        size.y = textHeight + padding;
        rectTransform.sizeDelta = size;
    }
}
