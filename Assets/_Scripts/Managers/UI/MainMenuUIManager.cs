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
        playButton = mainMenuScreen.Q<Button>(USSElementNames.MAIN_MENU_PLAY_BUTTON);
        settingsButton = mainMenuScreen.Q<Button>(USSElementNames.MAIN_MENU_SETTINGS_BUTTON);
        exitButton = mainMenuScreen.Q<Button>(USSElementNames.MAIN_MENU_EXIT_BUTTON);

        // SelectGame
        selectGameBackButton = selectGameScreen.Q<Button>(USSElementNames.SELECT_GAME_BACK_BUTTON);
        createGameButton = selectGameScreen.Q<Button>(USSElementNames.SELECT_GAME_CREATE_GAME_BUTTON);
        codeInputField = selectGameScreen.Q<IntegerField>(USSElementNames.SELECT_GAME_CODE_INPUT_FIELD);
        joinGameButton = selectGameScreen.Q<Button>(USSElementNames.SELECT_GAME_JOIN_GAME_BUTTON);

        // Lobby
        lobbyBackButton = lobbyScreen.Q<Button>(USSElementNames.LOBBY_BACK_BUTTON);
        lobbyStartButton = lobbyScreen.Q<Button>(USSElementNames.LOBBY_START_BUTTON);
    }

    #endregion


    #region Events
    [Header("Events")]
    [SerializeField] public UnityEvent PlayButtonClicked;
    [SerializeField] public UnityEvent ExitButtonClicked;
    [SerializeField] public UnityEvent SelectGameBackButtonClicked;
    [SerializeField] public UnityEvent LobbyBackButtonClicked;

    void SubscribeToEvents()
    {
        playButton.clicked += OnPlayBtnClicked;
        exitButton.clicked += OnExitBtnClicked;

        createGameButton.clicked += OnCreateGameBtnClicked;
        selectGameBackButton.clicked += OnSelectGameBackBtnClicked;

        lobbyBackButton.clicked += OnLobbyBackBtnClicked;
        lobbyStartButton.clicked += OnLobbyStartBtnClicked;
    }

    void UnsubscribeToEvents()
    {
        playButton.clicked -= OnPlayBtnClicked;
        exitButton.clicked -= OnExitBtnClicked;

        createGameButton.clicked -= OnCreateGameBtnClicked;
        selectGameBackButton.clicked -= OnSelectGameBackBtnClicked;

        lobbyBackButton.clicked -= OnLobbyBackBtnClicked;
        lobbyStartButton.clicked -= OnLobbyStartBtnClicked;
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

    void OnCreateGameBtnClicked()
    {
        ShowElement(lobbyScreen);
        HideElement(mainMenuScreen);
        HideElement(selectGameScreen);
    }

    void OnSelectGameBackBtnClicked()
    {
        SelectGameBackButtonClicked?.Invoke();

        ShowElement(mainMenuScreen);
        HideElement(selectGameScreen);
        HideElement(lobbyScreen);
    }

    void OnLobbyBackBtnClicked()
    {
        LobbyBackButtonClicked?.Invoke();

        ShowElement(selectGameScreen);
        HideElement(mainMenuScreen);
        HideElement(lobbyScreen);
    }

    void OnLobbyStartBtnClicked()
    {
        GameManager.Instance.ChangeState(nameof(GameplayGameState));
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
