using UnityEngine;


public class MainMenuUIManager : MonoBehaviour
{
    #region Singleton
    static MainMenuUIManager instance;
    public static MainMenuUIManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("No MainMenuUIManager instance");
            return instance;
        }
    }


    void SetupSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    [Header("UI Pages reference")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject selectGameMenu;
    //[SerializeField] GameObject settingsMenu;

    void Awake()
    {
        SetupSingleton();
    }

    void Start()
    {
        
    }


    public void ShowMainMenu() => mainMenu.SetActive(true);
    
    public void HideMainMenu() => mainMenu.SetActive(false);

    public void ShowSelectGameMenu() => selectGameMenu.SetActive(true);
    public void HideSelectGameMenu() => selectGameMenu.SetActive(false);

}
