using UnityEngine;
using UnityEngine.InputSystem;

public class CustomerSpawner : MonoSingleton<CustomerSpawner>
{
    [SerializeField] private GameObject[] customerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Target References")]
    public Transform[] runTargets;
    public Transform[] heatTargets;

    private int spawnNum;
    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            spawnNum = Random.Range(0, spawnPoints.Length);
            CustomerSpawn(spawnNum);
        }
    }

    public void CustomerSpawn(int num)
    {
        int rnd = Random.Range(0, customerPrefab.Length);
        GameObject obj = Instantiate(customerPrefab[rnd], spawnPoints[num].position, Quaternion.identity);

        Customer customer = obj.GetComponent<Customer>();
        customer.runTargets = runTargets;
        customer.hitTagets = heatTargets;
    }
}
