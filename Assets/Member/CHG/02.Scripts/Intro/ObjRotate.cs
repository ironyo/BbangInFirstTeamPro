using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private bool _left;

    private void Update()
    {
        float rotationAmount = _rotateSpeed * Time.deltaTime;

        if (!_left)
        {
            rotationAmount = -rotationAmount;
        }

        transform.Rotate(0,0,rotationAmount);
    }
}
