using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrainAutoSpawner2D : MonoBehaviour
{
    public CarHeadMovement2D headMovement;
    public TrainChainFollower2D chain;
    public Transform carPrefab;
    public int carCount;

    private void Start()
    {
        if (headMovement == null) headMovement = Object.FindFirstObjectByType<CarHeadMovement2D>();
        if (chain == null) chain = Object.FindFirstObjectByType<TrainChainFollower2D>();
        if (headMovement == null || chain == null || carPrefab == null) return;

        chain.head = headMovement.transform;
        chain.cars ??= new List<Transform>();
        chain.cars.Clear();

        
    }

    private void Update()
    {
        // 초기 배치: 헤드 뒤로 linkLength 간격만큼
        Vector3 basePos = headMovement.transform.position;
        Vector3 back = -headMovement.transform.up * chain.linkLength;

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            Debug.Log("ddd");
            for (int i = 0; i < carCount; i++)
            {
                Transform tail = Instantiate(carPrefab, basePos + back * (i + 1), headMovement.transform.rotation);
                chain.cars.Add(tail);
            }
        }
    }
}