using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverGameState : BaseGameState
{
    /// <summary>
    /// Actions to setup state
    /// </summary>
    public override async void EnterState()
    {
        // Check if current scene is gameplay
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
        {
            return;
        }

        // Stop Time
        Time.timeScale = 0;

        // Show GameOver screen
        //UIManager.Instance.ShowGameOverScreen();

        // TODO: add Exit btn to GameOver screen, change this method to not async
        await Task.Delay(3000);
        GameManager.Instance.ChangeState(nameof(MainMenuGameState));
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
        // Resume Time
        Time.timeScale = 1;

        // TODO: Hide GameOver screen (not necessery? - scene will be destroyed)
    }
}