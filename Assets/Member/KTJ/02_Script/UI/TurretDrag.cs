using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurretDrag : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private GameObject _dragPref;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GraphicRaycaster _raycaster;

    [Header("Events")]
    [SerializeField] private EventChannelSO_T<TurretSO_TJ> _onLabelClick;

    private RectTransform _currentDragTur = null;
    private TurretSO_TJ _currentSelectTur = null;

    private void Awake()
    {
        _onLabelClick.OnEventRaised += StartDrag;
    }

    private void StartDrag(TurretSO_TJ data)
    {
        Debug.Log("제발 여기까지만 제발제발");
        GameObject spawned = Instantiate(_dragPref, _canvas.transform);

        Image img = spawned.GetComponent<Image>();
        _currentDragTur = spawned.GetComponent<RectTransform>();
        _currentSelectTur = data;

        img.sprite = data.TurretImage;
        Debug.Log("aaaaaaa");
    }

    private void Update()
    {
        if (_currentDragTur == null) return;

        if (Mouse.current.leftButton.isPressed)
        {
            _currentDragTur.position = Mouse.current.position.ReadValue();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            CheckDrop();
            Destroy(_currentDragTur.gameObject);
            _currentDragTur = null;
            _currentSelectTur = null;
        }
    }

    private void CheckDrop()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(eventData, results);

        foreach (var r in results)
        {
            if (r.gameObject.TryGetComponent<TruckSlot>(out TruckSlot slot))
            {
                slot.DropTurret(_currentSelectTur);
                return;
            }
        }
    }
}
