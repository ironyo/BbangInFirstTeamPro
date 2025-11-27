using UnityEngine;

public class KU_Source : MonoBehaviour
{
    private Rigidbody2D _rigidCompo;

    private void Awake()
    {
        _rigidCompo = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }
}
