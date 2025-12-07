using UnityEngine;

public abstract class Store : MonoBehaviour
{
    public abstract StoreEnum StoreType { get; }
    [SerializeField] private Store_UI _storeUI;

    private void Awake()
    {
        if (_storeUI == null)
        {
            Debug.LogError("_storeUI가 인스펙터에서 할당되지 않았습니다. 확인바랍니다.");
        }
    }
    public virtual void Enter()
    {
        _storeUI.OpenUI();

        CameraEffectManager.Instance.CameraZoom(5f, 0.1f);
        GlobalVolumeManager.Instance.SetDof(true);
    }

    public virtual void Exit()   
    {
        _storeUI.CloseUI();
        StoreManager_TJ.Instance.Exit();

        CameraEffectManager.Instance.CameraZoom(2f, 0.1f);
        GlobalVolumeManager.Instance.SetDof(false);
    }
}
