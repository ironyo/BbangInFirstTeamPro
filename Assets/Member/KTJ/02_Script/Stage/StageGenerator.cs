using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private string[] stageNames;
    [SerializeField] private int minLength;
    [SerializeField] private int maxLength;

    public StageData CreateRandomStage()
    {
        return StageData.Create(
            stageNames[Random.Range(0, stageNames.Length)],
            Random.Range(minLength, maxLength)
        );
    }
}
