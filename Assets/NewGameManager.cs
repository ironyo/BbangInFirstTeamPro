using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    [SerializeField] private StageChannelInt stageChannelInt;

    public void NewGame()
    {
        stageChannelInt.ResetStage();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
