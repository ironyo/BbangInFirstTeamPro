using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class KU_Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbodyCompo;

    private Vector2 _moveDir;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _redSourcePref;
    [SerializeField] private GameObject _yellowSourcePref;

    private bool nowSourceFlag = true;

    private Vector3 rotationSpeed = new Vector3(0, 0, 10);

    private CancellationTokenSource cancellationTokenSource;


    private void Awake()
    {
        _rigidbodyCompo = GetComponent<Rigidbody2D>();
        cancellationTokenSource = new CancellationTokenSource();
    }
    private void Start()
    {
        SpawnSource().Forget();
    }

    private void Update()
    {
        _rigidbodyCompo.linearVelocity += _moveDir.normalized * Time.deltaTime * _moveSpeed;
        transform.rotation *= Quaternion.Euler(rotationSpeed);
    }   
    public void GetTarget(Transform pos)
    {
        _moveDir = pos.position - transform.position;
    }

    private async UniTask SpawnSource()
    {
        try
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(0.3f, cancellationToken: cancellationTokenSource.Token);

                if (this == null) return;
                if (transform == null) return;

                GameObject obj = Instantiate(
                    nowSourceFlag ? _redSourcePref : _yellowSourcePref, 
                    transform.position, 
                    transform.rotation);

                nowSourceFlag = !nowSourceFlag;

                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = transform.right * _moveSpeed;
            }
        }
        catch (System.OperationCanceledException)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<KU_Enemy>(out KU_Enemy enemy))
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
    }
}