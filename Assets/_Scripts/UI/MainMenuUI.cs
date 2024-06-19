using UnityEngine;


public class MainMenuUI : MonoBehaviour
{
    public void OnPlayClicked()
    {
        MainMenuUIManager.Instance.ShowSelectGameMenu();
        MainMenuUIManager.Instance.HideMainMenu();
    }


    public void OnExitClicked()
    {
        Application.Quit();
    }


}