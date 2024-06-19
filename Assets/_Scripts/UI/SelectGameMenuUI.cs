using UnityEngine;


public class SelectGameMenuUI : MonoBehaviour
{
    public void OnCreateGameClicked()
    {
        GameManager.Instance.ChangeState(nameof(GameplayGameState));
    }

    public void OnJoinClicked()
    {
        /* ToDo:
         * Get game code from input
         * Try to connect to game
         * Change state to gameplay
         */

    }

    public void OnBackClicked()
    {
        MainMenuUIManager.Instance.HideSelectGameMenu();
        MainMenuUIManager.Instance.ShowMainMenu();

    }


}
