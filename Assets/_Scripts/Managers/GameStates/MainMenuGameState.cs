using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuGameState : BaseGameState
{
    /// <summary>
    /// Actions to setup state
    /// </summary>
    public override void EnterState()
    {
        SceneManager.LoadScene(0);
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
        // TODO: Open loading screen
    }
}