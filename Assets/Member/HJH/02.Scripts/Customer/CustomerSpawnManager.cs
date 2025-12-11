using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomerSpawnManager : MonoBehaviour
{
    [SerializeField] private CustomerSpawner customerSpawner;
    [Header("Events")]
    [SerializeField] private EventChannelSO _onStageRoadEnd;
    [SerializeField] private EventChannelSO_T<int> _onStageRoadStart;

    private bool isSpawning = false;

    public float spawnInterval { get; set; } = 1.0f;
    private void Start()
    {
        _onStageRoadEnd.OnEventRaised += AllClearCustomer;

        _onStageRoadStart.OnEventRaised += AA;
    }

    public void AllClearCustomer()
    {
        Customer[] customers = (Customer[])FindObjectsByType<Customer>(FindObjectsSortMode.None);

        foreach (var c in customers)
        {
            c.RequestClear();
        }

        isSpawning = false;
    }

    public void CustomerSpawner(float fixA, float fixB, float fixC, float fixIntervel)
    {
        customerSpawner.SetWeights(fixA,fixB,fixC);
        spawnInterval = fixIntervel;
    }

    private void AA(int v)
    {
        StartCoroutine(SpawnCustomer());
    }

    public IEnumerator SpawnCustomer()
    {
        isSpawning = true;

        while (isSpawning)
        {
            customerSpawner.StartSpawn();
            yield return new WaitForSeconds(spawnInterval);  
        }
    }
}
