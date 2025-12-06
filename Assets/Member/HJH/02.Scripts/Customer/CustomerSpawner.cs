using UnityEngine;
using UnityEngine.InputSystem;

public class CustomerSpawner : MonoSingleton<CustomerSpawner>
{
    [SerializeField] private GameObject[] customerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private CustomerTypeList typeList;

    [Header("Target References")]
    public Transform[] runTargets;
    public Transform[] heatTargets;

    private int spawnNum;
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            spawnNum = Random.Range(0, spawnPoints.Length);
            CustomerSpawn(spawnNum);
        }
    }

    public void CustomerSpawn(int num)
    {
        int rnd = Random.Range(1, 100);

        Debug.Log("rnd 값이" +  rnd);

        float wA = typeList.customerTypes[0].weight;
        float wB = typeList.customerTypes[1].weight;
        float wC = typeList.customerTypes[2].weight;

        float sumCustomer = wA;
        float sumFat = wA + wB;
        float sumUnkind = wA + wB + wC;

        int customerSpawnNum = 0;

        if (rnd <= sumCustomer)
        {
            customerSpawnNum = 0;
        }
        else if (rnd <= sumFat)
        {
            customerSpawnNum = 1;
        }
        else if (rnd <= sumUnkind)
        {
            customerSpawnNum = 2;
        }

        GameObject obj = Instantiate(customerPrefab[customerSpawnNum], spawnPoints[num].position, Quaternion.identity);

        Customer customer = obj.GetComponent<Customer>();
        customer.runTargets = runTargets;
        customer.hitTagets = heatTargets;
    }


    // 이 메서드는 시간이 지날 수록 밸런스 패치할 때 쓰면 됨
    public void SetWeights(float wA, float wB, float wC)
    {
        typeList.customerTypes[0].weight = wA;
        typeList.customerTypes[1].weight = wB;
        typeList.customerTypes[2].weight = wC;
    }
}
