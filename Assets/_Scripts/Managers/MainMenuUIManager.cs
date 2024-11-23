using UnityEditor;
using UnityEditor.UIElements;
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


    #region VisualElements
    [SerializeField] UIDocument uiDocument;

    VisualElement root;
    VisualElement mainMenuScreen;
    VisualElement selectGameScreen;
    VisualElement lobbyScreen;

    // Main Menu Screen
    Button playButton;
    Button settingsButton;
    Button exitButton;
    
    // Select Game Screen
    Button selectGameBackButton;
    Button createGameButton;
    IntegerField codeInputField;
    Button joinGameButton;

    // Lobby Screen
    Button lobbyBackButton;
    Button lobbyStartButton;
    

    void SetupVisualElements()
    {
        root = uiDocument.rootVisualElement;

        // Screens
        mainMenuScreen = root.Q(USSElementNames.MAIN_MENU_SCREEN);
        selectGameScreen = root.Q(USSElementNames.SELECT_GAME_SCREEN);
        lobbyScreen = root.Q(USSElementNames.LOBBY_SCREEN);

        // MainMenu
        playButton = root.Q<Button>(USSElementNames.MAIN_MENU_PLAY_BUTTON);
        settingsButton = root.Q<Button>(USSElementNames.MAIN_MENU_SETTINGS_BUTTON);
        exitButton = root.Q<Button>(USSElementNames.MAIN_MENU_EXIT_BUTTON);

        // SelectGame
        selectGameBackButton = root.Q<Button>(USSElementNames.SELECT_GAME_BACK_BUTTON);
        createGameButton = root.Q<Button>(USSElementNames.SELECT_GAME_CREATE_GAME_BUTTON);
        codeInputField = root.Q<IntegerField>(USSElementNames.SELECT_GAME_CODE_INPUT_FIELD);
        joinGameButton = root.Q<Button>(USSElementNames.SELECT_GAME_JOIN_GAME_BUTTON);

        // Lobby
        lobbyBackButton = root.Q<Button>(USSElementNames.LOBBY_BACK_BUTTON);
        lobbyStartButton = root.Q<Button>(USSElementNames.LOBBY_START_BUTTON);
    }

    #endregion


    #region Events
    [Header("Events")]
    [SerializeField] public UnityEvent PlayButtonClicked;
    [SerializeField] public UnityEvent ExitButtonClicked;
    [SerializeField] public UnityEvent SelectGameBackButtonClicked;

    void SubscribeToEvents()
    {
        playButton.clicked += OnPlayBtnClicked;
        exitButton.clicked += OnExitBtnClicked;
        selectGameBackButton.clicked += OnBackBtnClicked;
    }

    void UnsubscribeToEvents()
    {
        playButton.clicked -= OnPlayBtnClicked;
        exitButton.clicked += OnExitBtnClicked;
        selectGameBackButton.clicked -= OnBackBtnClicked;
    }


    // Event callbacks?
    void OnPlayBtnClicked()
    {
        PlayButtonClicked?.Invoke();

        ShowElement(selectGameScreen);
        HideElement(mainMenuScreen);
    }

    void OnExitBtnClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    void OnBackBtnClicked()
    {
        SelectGameBackButtonClicked?.Invoke();

        ShowElement(mainMenuScreen);
        HideElement(selectGameScreen);
        HideElement(lobbyScreen);
    }

    #endregion


    #region MonoBehaviour
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
    #endregion


    void ShowElement(VisualElement visualElement)
    {
        visualElement.style.display = DisplayStyle.Flex;
    }

    void HideElement(VisualElement visualElement)
    {
        visualElement.style.display = DisplayStyle.None;
    }

}
