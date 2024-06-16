using UnityEngine;
using UnityEngine.SceneManagement;


public class GameplayGameState : BaseGameState
{
    /// <summary>
    /// Actions to setup state
    /// </summary>
    public override void EnterState()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            return;
        }

        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// UpdateState
    /// </summary>
    public override void UpdateState() { }

    /// <summary>
    /// Actions to close and exit state
    /// </summary>
    public override void ExitState()
    {

    }
}