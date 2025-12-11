using Assets.Member.CHG._02.Scripts.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoSingleton<ToolTipManager>
{
    [SerializeField] private ToolTipUI tooltipUIPrefab;

    Factory factory;

    private readonly List<ToolTipUI> active = new List<ToolTipUI>();

    private void Start()
    {
        factory = new Factory(tooltipUIPrefab.gameObject, 6);
    }

    public void ShowToolTip(string text)
    {
        if (active.Count >= 3)
        {
            ToolTipUI oldest = active[0];
            active.RemoveAt(0);
            oldest.DestroyUI();
        }

        IRecycleObject tooltip = factory.Get();
        tooltip.GameObject.transform.SetParent(transform, false);
        ToolTipUI ToolTipUI = tooltip.GameObject.GetComponent<ToolTipUI>();
        ToolTipUI.SetText(text);
        active.Add(ToolTipUI);

        ReMove();
    }

    private void ReMove()
    {
        StartCoroutine(DelayedReMove());
    }

    public IEnumerator DelayedReMove()
    {
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < active.Count; i++)
        {
            int index = active.Count - i - 1;
            float y = new Vector3(0, -300, 0).y + (100f * index);
            active[i].Move(new Vector3(0, y, 0));
        }
    }

    public void RemoveActive(ToolTipUI toolTipUI)
    {
        active.Remove(toolTipUI);
        ReMove();
    }

    public void HideToolTip()
    {
        ShowToolTip("나는 멍청이입니다.");
    }
}
