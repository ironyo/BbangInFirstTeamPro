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

    public float spawnInterval = 5.0f; //{ get; set; } = 5.0f;
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

    public void CustomerSpawner(float fixA, float fixB, float fixC,float fixD, float fixIntervel)
    {
        customerSpawner.SetWeights(fixA,fixB,fixC,fixD);
        spawnInterval = fixIntervel;
    }

    private void AA(int v)
    {
        int r = Random.Range(1,3);
        StartCoroutine(SpawnCustomer(r));
    }

    public IEnumerator SpawnCustomer(int random)
    {
        isSpawning = true;

        while (isSpawning)
        {
            for(int i = 0; i < random; i++)
            {
                customerSpawner.StartSpawn();
            }
            yield return new WaitForSeconds(spawnInterval);  
        }
    }
}

