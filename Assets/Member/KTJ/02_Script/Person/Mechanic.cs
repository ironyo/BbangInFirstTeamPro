using System.Threading.Tasks;
using UnityEngine;

public class Mechanic : Person
{
    protected override void Clicked()
    {
        CameraEffectManager.Instance.CameraMoveTarget(gameObject);
        CameraEffectManager.Instance.CameraZoom(2f, 0.1f);
    }

    protected override void UnClicked()
    {
        CameraEffectManager.Instance.CameraMoveTarget(CameraEffectManager.Instance.CameraTarget.gameObject);
        CameraEffectManager.Instance.CameraZoom(7f, 0.1f);
    }
}
