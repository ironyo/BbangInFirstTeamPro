using System.Threading.Tasks;
using UnityEngine;

public class EnemySlowSkill : MonoBehaviour
{
    private Customer customer;
    private float _currentTime;

    [SerializeField] ItemDataSO _data;

    private void Awake()
    {
        Customer[] customers = FindObjectsOfType<Customer>();
    }

    private void OnEnable()
    {
        customer.OnSlowChanged += customer.HandleSlow;
    }
    private void Start()
    {
        //customer.OnSlowChanged?.Invoke(true);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _data.Duration)
        {
            TimeEnd();
        }
    }
    private void TimeEnd()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        customer.OnSlowChanged -= customer.HandleSlow;
    }
}