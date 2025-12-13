using System.Threading.Tasks;
using UnityEngine;

public class EnemySlowSkill : MonoBehaviour
{
    private Customer customer;

    private void Awake()
    {
        customer = GetComponent<Customer>();
    }

    private void OnEnable()
    {
        customer.OnSlowChanged += customer.HandleSlow;
    }
    private void Start()
    {
        //customer.OnSlowChanged.Invoke(true);
    }

    private void OnDisable()
    {
        customer.OnSlowChanged -= customer.HandleSlow;
    }
}