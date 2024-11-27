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
    [Header("Visual Tree Template")]
    [SerializeField] VisualTreeAsset gameCard;

    VisualElement root;
    VisualElement gameplayScreen;
    VisualElement pauseMenuScreen;

    // Gameplay HUD
    Button pauseButton;

    // TMP
    VisualElement deckCards;
    
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

        
        // PauseMenu
        resumeButton = pauseMenuScreen.Q<Button>(USSElementNames.PAUSE_MENU_RESUME_BUTTON);
        settingsButton = pauseMenuScreen.Q<Button>(USSElementNames.PAUSE_MENU_SETTINGS_BUTTON);
        exitButton = pauseMenuScreen.Q<Button>(USSElementNames.PAUSE_MENU_EXIT_BUTTON);

    }

    void LateSetupVisualElements()
    {
        // TMP
        //var list = GameplayManager.Instance.DeckCards;
        //deckCards = gameplayScreen.Q("DeckCards");
        //var card = new VisualElement();
        //card.Add(gameCard.Instantiate());
        //deckCards.makeItem = () => card;
        ////deckCards.bindItem = (e, i) => (e as Label).text = list[i].Id.ToString();
        //deckCards.bindItem = (e, i) =>
        //{
        //    var gameCard = list[i];
        //    var cardRoot = e as VisualElement;
        //    cardRoot.Q<Label>("CardType").text = gameCard.Name;
        //    cardRoot.Q<Label>("CardValue").text = gameCard.Name;

        //};
        //deckCards.itemsSource = list;
        //deckCards.itemsChosen += (e) => Debug.Log($"Chosen {e}");


        var cardList = GameplayManager.Instance.DeckCards;
        deckCards = gameplayScreen.Q("DeckCards");
        deckCards.Clear();

        foreach (var card in cardList)
        {
            var cardVisual = gameCard.Instantiate();
            cardVisual.Q<Label>("CardValue").text = card.Name;
            deckCards.Add(cardVisual);            
        }
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

    }

    void UnsubscribeToEvents()
    {
        pauseButton.clicked -= OnPauseBtnClicked;

        resumeButton.clicked -= OnResumeBtnClicked;
        exitButton.clicked -= OnExitBtnClicked;


        // TMP
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
        // TMP
        LateSetupVisualElements();
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
