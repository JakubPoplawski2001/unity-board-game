using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class GameplayUIManager : MonoBehaviour
{
    #region Singleton
    static GameplayUIManager instance;
    public static GameplayUIManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("No GameplayUIManager instance");
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
    VisualElement gameplayScreen;
    VisualElement pauseMenuScreen;

    // Gameplay HUD
    Button pauseButton;

    // TMP
    Button pickButton;
    Button useButton;
    
    // Pause Menu Screen
    Button resumeButton;
    Button settingsButton;
    Button exitButton;
        

    void SetupVisualElements()
    {
        root = uiDocument.rootVisualElement;

        // Screens
        pauseMenuScreen = root.Q(USSElementNames.PAUSE_MENU_SCREEN);
        gameplayScreen = root.Q(USSElementNames.GAMEPLAY_SCREEN);

        // Gameplay HUD
        pauseButton = gameplayScreen.Q<Button>(USSElementNames.GAMEPLAY_PAUSE_BUTTON);

        // TMP
        pickButton = gameplayScreen.Q<Button>("TMPPickButton");
        useButton = gameplayScreen.Q<Button>("TMPUseButton");

        // PauseMenu
        resumeButton = pauseMenuScreen.Q<Button>(USSElementNames.PAUSE_MENU_RESUME_BUTTON);
        settingsButton = pauseMenuScreen.Q<Button>(USSElementNames.PAUSE_MENU_SETTINGS_BUTTON);
        exitButton = pauseMenuScreen.Q<Button>(USSElementNames.PAUSE_MENU_EXIT_BUTTON);

    }

    #endregion


    #region Events
    [Header("Events")]
    [SerializeField] public UnityEvent PauseButtonClicked;

    // TMP
    [SerializeField] public UnityEvent PickButtonClicked;
    [SerializeField] public UnityEvent UseButtonClicked;


    [SerializeField] public UnityEvent ResumeButtonClicked;
    [SerializeField] public UnityEvent ExitButtonClicked;


    void SubscribeToEvents()
    {
        pauseButton.clicked += OnPauseBtnClicked;

        resumeButton.clicked += OnResumeBtnClicked;
        exitButton.clicked += OnExitBtnClicked;


        // TMP
        pickButton.clicked += OnPickBtnClicked;
        useButton.clicked += OnUseBtnClicked;

    }

    void UnsubscribeToEvents()
    {
        pauseButton.clicked -= OnPauseBtnClicked;

        resumeButton.clicked -= OnResumeBtnClicked;
        exitButton.clicked -= OnExitBtnClicked;


        // TMP
        pickButton.clicked -= OnPickBtnClicked;
        useButton.clicked -= OnUseBtnClicked;
    }


    // Event callbacks?
    void OnPauseBtnClicked()
    {
        PauseButtonClicked?.Invoke();

        ShowElement(pauseMenuScreen);
    }

    void OnResumeBtnClicked()
    {
        ResumeButtonClicked?.Invoke();

        HideElement(pauseMenuScreen);
    }

    void OnExitBtnClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }


    // TMP
    void OnPickBtnClicked()
    {
        PickButtonClicked?.Invoke();


    }

    void OnUseBtnClicked()
    {
        UseButtonClicked?.Invoke();


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
