using TMPro;
using UnityEngine.UI;

public class ToolTipManager : MonoSingleton<ToolTipManager>
{
    private Image image;
    private TextMeshProUGUI text;

    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowToolTip(string text)
    {
        this.text.text = text;

    }

    public void HideToolTip()
    {
        ShowToolTip("나는 멍청이입니다.");
    }
}
