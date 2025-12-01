using System.Threading.Tasks;
using UnityEngine;

public class Mechanic : Person
{
    protected override void Clicked()
    {
        CameraEffectManager.Instance.CameraMoveTarget(gameObject);
        CameraEffectManager.Instance.CameraZoom(2f, 0.4f);
    }
}
