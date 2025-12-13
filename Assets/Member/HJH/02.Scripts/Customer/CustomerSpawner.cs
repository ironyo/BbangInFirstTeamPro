using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomerSpawner : MonoSingleton<CustomerSpawner>
{
    public float _difficultyMultiplier = 1f;

    [SerializeField] private GameObject[] customerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private CustomerTypeList typeList;

    [SerializeField] private StageChannelInt _stageChannelInt;

    [Header("Target References")]
    public Transform[] runTargets;
    public Transform[] heatTargets;

    private int spawnNum;
    private void OnEnable()
    {
        _stageChannelInt.OnEventRaised += HandleStageDifficulty;
    }

    private void OnDisable()
    {
        _stageChannelInt.OnEventRaised -= HandleStageDifficulty;
    }
    public void StartSpawn()
    {
        spawnNum = Random.Range(0, spawnPoints.Length);
        CustomerSpawn(spawnNum);
    }

    private void Update()
    {
        HandleStageDifficulty(_stageChannelInt.CurrentStage);
    }
    public void AddTargets(GameObject parent)
    {
        List<Transform> runList = new List<Transform>();
        List<Transform> heatList = new List<Transform>();

        foreach (Transform child in parent.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("RunTarget"))
                runList.Add(child);
            else if (child.CompareTag("CloseTarget"))
                heatList.Add(child);
        }

        runTargets = runList.ToArray();
        heatTargets = heatList.ToArray();
    }

    public void CustomerSpawn(int num)
    {
        int rnd = Random.Range(1, 100);

        Debug.Log("rnd °ªÀÌ: " + rnd);

        float wA = typeList.customerTypes[0].weight;
        float wB = typeList.customerTypes[1].weight;
        float wC = typeList.customerTypes[2].weight;
        float wD = typeList.customerTypes[3].weight;

        float sumCustomer = wA;
        float sumFat = wA + wB;
        float sumUnkind = wA + wB + wC;
        float sumTrash = wA + wB + wC + wD;

        int customerSpawnNum = 0;

        if (rnd <= sumCustomer)
            customerSpawnNum = 0;
        else if (rnd <= sumFat)
            customerSpawnNum = 1;
        else if (rnd <= sumUnkind)
            customerSpawnNum = 2;
        else if (rnd <= sumTrash)
            customerSpawnNum = 3;

        GameObject obj = Instantiate(customerPrefab[customerSpawnNum], spawnPoints[num].position, Quaternion.identity);

        Customer customer = obj.GetComponent<Customer>();
        customer.runTargets = runTargets;
        customer.hitTagets = heatTargets;

        customer.OnClearRequested += customer.HandleClearRequested;
    }

    public void AllClearCustomer()
    {
        Customer[] customers = (Customer[])FindObjectsByType<Customer>(FindObjectsSortMode.None);
        foreach (var c in customers)
        {
            c.RequestClear();
        }
    }

    public void SetWeights(float wA, float wB, float wC, float wD)
    {
        typeList.customerTypes[0].weight = wA;
        typeList.customerTypes[1].weight = wB;
        typeList.customerTypes[2].weight = wC;
        typeList.customerTypes[3].weight = wD;
    }
    private void HandleStageDifficulty(int stageCount)
    {
        _difficultyMultiplier = 1f + stageCount * 0.15f;
    }
}
