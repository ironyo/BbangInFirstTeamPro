using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StageInfo
{
    public string previousStageName;
    public string currentStageName;
    public int roadLength;
}

public class StageManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roadLengthTxt;
    [SerializeField] private TextMeshProUGUI previousStageTxt;
    [SerializeField] private TextMeshProUGUI currentStageTxt;
    [SerializeField] private Slider truckPointSlider;

    [Header("Settings")]
    [SerializeField] private string[] StageNames;

    [Range(0, 300)]
    [SerializeField] private int MaxRoadLength;
    [Range(0, 300)]
    [SerializeField] private int MinRoadLength;

    private StageInfo currentStage;

    public UnityEvent<bool> OnTruckDriving;

    private void Start()
    {
        StageInfo newStage = PickNextVillage();
        SetStage(newStage.roadLength, newStage.previousStageName, newStage.currentStageName);
    }

    private void SetStage(int totalLength, string previousStageName, string currentStageName)
    {
        roadLengthTxt.text = "남은거리: " + totalLength.ToString() + "m";
        previousStageTxt.text = previousStageName;
        currentStageTxt.text = currentStageName;
        truckPointSlider.value = 0;
    }

    private StageInfo PickNextVillage()
    {
        string randomStage = StageNames[Random.Range(0, StageNames.Length)];
        StageInfo newStage = new StageInfo();

        if (currentStage == null)
        {
            newStage.previousStageName = "출발지점";
        }
        else
        {
            newStage.previousStageName = currentStage?.currentStageName;
        }

        newStage.currentStageName = randomStage;
        newStage.roadLength = Random.Range(MinRoadLength, MaxRoadLength);

        return newStage;
    }
}