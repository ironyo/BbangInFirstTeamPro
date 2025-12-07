using Unity.Cinemachine;

public class CameraShake : MonoSingleton<CameraShake>
{
    private CinemachineImpulseSource _impulseSource;

    protected override void Awake()
    {
        base.Awake();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ImpulseForce(float force)
    {
        if (_impulseSource != null)
        {
            _impulseSource.GenerateImpulse(force);
        }
    }
}
