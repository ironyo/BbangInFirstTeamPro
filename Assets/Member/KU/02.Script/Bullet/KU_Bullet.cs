using System.Collections;
using UnityEngine;

public abstract class KU_Bullet : MonoBehaviour
{
    public Rigidbody2D _rigidbodyCompo { get; private set; }

    private Vector2 _moveDir;
    public float moveSpeed { get; private set; } = 5;

    private Vector3 rotationSpeed = new Vector3(0, 0, 5);

    private float _lifeTime = 3;

    public KU_Enemy targetEnemy { get; private set; }
    private bool _nowTargetSet = false;

    [SerializeField] private GameObject _moveParticlePref;
    [SerializeField] private GameObject _boomParticlePref;
    [SerializeField] private Transform _particlePoint;


    protected virtual void Awake()
    {
        _rigidbodyCompo = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Instantiate(_moveParticlePref, _particlePoint.transform.position, Quaternion.identity, _particlePoint.transform);
    }
    public virtual void Update()
    {
        transform.rotation *= Quaternion.Euler(rotationSpeed);
        if(targetEnemy != null && _nowTargetSet)
        {
            StartCoroutine(LifeTime());
        }
    }
    public void BoomParticle()
    {
        Instantiate(_boomParticlePref, transform.position, Quaternion.identity);
    }
    public void GetTarget(KU_Enemy pos)
    {
        targetEnemy = pos;
        _moveDir = pos.gameObject.transform.position - transform.position;
        _rigidbodyCompo.linearVelocity = _moveDir.normalized * moveSpeed;
        _nowTargetSet = true;
    }

    public void StopBullet()
    {
        rotationSpeed = Vector3.zero;
        moveSpeed = 0;
        _rigidbodyCompo.linearVelocity = Vector3.zero;
    }
    
    public IEnumerator LifeTime()
    {
        _nowTargetSet = false;
        StopAllCoroutines();
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
