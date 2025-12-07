using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeManager : MonoSingleton<GlobalVolumeManager>
{
    [SerializeField] private Volume _globalVolume;
    private DepthOfField _depthOfField;

    private void Start()
    {
        if (_globalVolume.profile.TryGet(out DepthOfField depthOfField))
        {
            _depthOfField = depthOfField;
        }
        else
        {
            Debug.LogError("Depth of Field가 Volume Profile에 없음");
        }
    }

    public void SetDof(bool value)
    {
        _depthOfField.active = value;
    }
}
