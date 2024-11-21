using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


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

    [Header("Reference")]
    [SerializeField] UIDocument uiDocument;


    [Header("Events")]
    [SerializeField] public UnityEvent<bool> btnClick;
    [SerializeField] public UnityEvent<bool> btnClick2;


    void Awake()
    {
        SetupSingleton();
    }

    void Start()
    {
        //var screen = uiDocument.rootVisualElement.Q<VisualElement>(className: StringNames.USS.screen);
        //StringNames.USS.screen = "screen";
        //screen.style.display = DisplayStyle.None;
    }


    void SetupVisualElements()
    {

    }

    //public void ShowMainMenu() => mainMenu.SetActive(true);
    
    //public void HideMainMenu() => mainMenu.SetActive(false);

    //public void ShowSelectGameMenu() => selectGameMenu.SetActive(true);
    //public void HideSelectGameMenu() => selectGameMenu.SetActive(false);

    //public void ShowGameLobby() => gameLobby.SetActive(true);

    //public void HideGameLobby() => gameLobby.SetActive(false);

}
