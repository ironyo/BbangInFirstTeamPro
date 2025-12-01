using UnityEngine;

public class Mechanic : Person
{
    protected override void Clicked()
    {
        CameraEffectManager.Instance.CameraZoom(1f, 1f);
        CameraEffectManager.Instance.CameraMoveTarget(gameObject.transform.position, 1f);
    }
}
