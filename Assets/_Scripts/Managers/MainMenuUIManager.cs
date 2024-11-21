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
    [SerializeField] public UnityEvent PlayButtonClicked;
    [SerializeField] public UnityEvent BackButtonClicked;

    // VisualElements
    VisualElement root;
    VisualElement mainMenuScreen;
    VisualElement selectGameScreen;
    VisualElement lobbyScreen;

    Button playButton;
    Button settingsButton;
    Button exitButton;
    Button backButton;


    // MonoBehaviour methods
    void Awake()
    {
        SetupSingleton();
        SetupVisualElements();
    }

    void Start()
    {
    }

    void OnEnable()
    {
        SubscribeToEvents();
    }

    void OnDisable()
    {
        UnsubscribeToEvents();
    }



    void SetupVisualElements()
    {
        root = uiDocument.rootVisualElement;

        // Screens
        mainMenuScreen = root.Q(USSElementNames.MAIN_MENU_SCREEN);
        selectGameScreen = root.Q(USSElementNames.SELECT_GAME_SCREEN);
        lobbyScreen = root.Q(USSElementNames.LOBBY_SCREEN);

        // MainMenu
        playButton = root.Q<Button>(USSElementNames.PLAY_BUTTON);
        settingsButton = root.Q<Button>(USSElementNames.SETTINGS_BUTTON);
        exitButton = root.Q<Button>(USSElementNames.EXIT_BUTTON);

        // SelectGame
        backButton = root.Q<Button>(USSElementNames.BACK_BUTTON);
    }

    void SubscribeToEvents()
    {
        playButton.clicked += OnPlayBtnClicked;
        backButton.clicked += OnBackBtnClicked;
    }

    void UnsubscribeToEvents()
    {
        playButton.clicked -= OnPlayBtnClicked;
        backButton.clicked -= OnBackBtnClicked;
    }


    // Event callbacks?
    void OnPlayBtnClicked()
    {
        PlayButtonClicked?.Invoke();

        ShowElement(selectGameScreen);
        HideElement(mainMenuScreen);
    }

    void OnBackBtnClicked()
    {
        BackButtonClicked?.Invoke();

        ShowElement(mainMenuScreen);
        HideElement(selectGameScreen);
        HideElement(lobbyScreen);
    }




    void ShowElement(VisualElement visualElement)
    {
        visualElement.style.display = DisplayStyle.Flex;
    }

    void HideElement(VisualElement visualElement)
    {
        visualElement.style.display = DisplayStyle.None;
    }

}
