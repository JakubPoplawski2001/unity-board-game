using UnityEngine;


public class GameLobbyUI : MonoBehaviour
{
    public void OnStartGameClicked()
    {
        GameManager.Instance.ChangeState(nameof(GameplayGameState));
    }


    public void OnBackClicked()
    {
        MainMenuUIManager.Instance.HideGameLobby();
        MainMenuUIManager.Instance.ShowMainMenu();

    }
}
