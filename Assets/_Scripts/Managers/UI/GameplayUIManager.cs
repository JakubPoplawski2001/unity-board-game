using System.Collections.Generic;
using System.Linq;
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
    CardListView deckCards;
    Button pickCardsButton;
    CardListView handCards;
    Button useCardButton;
    Button dropCardButton;

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

        pickCardsButton = gameplayScreen.Q<Button>(USSElementNames.GAMEPLAY_PICK_CARDS_BUTTON);

        deckCards = new CardListView(
            gameplayScreen.Q(USSElementNames.GAMEPLAY_DECK_CARDS_LIST),
            2,
            gameCard);
        deckCards.AddOnItemClickedAction((cardVisual, card) =>
        {
            var fxVisual = cardVisual.Q(USSElementNames.CARD_SELECTED_FX);

            if (fxVisual.style.display == DisplayStyle.None) ShowElement(fxVisual);
            else HideElement(fxVisual);
        });
        deckCards.AddOnMaxItemSelectedAction((isMaxReached) =>
        {
            if (isMaxReached) ShowElement(pickCardsButton);
            else HideElement(pickCardsButton);
        });
        deckCards.AddOnUpdateItemsAction(OnCardsUpdated);

        useCardButton = gameplayScreen.Q<Button>(USSElementNames.GAMEPLAY_USE_CARD_BUTTON);
        dropCardButton = gameplayScreen.Q<Button>(USSElementNames.GAMEPLAY_DROP_CARD_BUTTON);

        handCards = new CardListView(
            gameplayScreen.Q(USSElementNames.GAMEPLAY_HAND_CARDS_LIST),
            1,
            gameCard);
        handCards.AddOnItemClickedAction((cardVisual, card) =>
        {
            var focusedClassName = USSClasses.CARD_FOCUSED;
            if (cardVisual.ClassListContains(focusedClassName)) cardVisual.RemoveFromClassList(focusedClassName);
            else cardVisual.AddToClassList(focusedClassName);
        });
        handCards.AddOnMaxItemSelectedAction( (isMaxReached) =>
        {
            if (isMaxReached)
            {
                // Check if card can be used
                if (handCards.GetSelectedItems().All(c => c.CanBeUsed))
                {
                    ShowElement(useCardButton);
                }
                else
                {
                    HideElement(useCardButton);
                }
                ShowElement(dropCardButton);
            }
            else
            {
                HideElement(useCardButton);
                HideElement(dropCardButton);
            }
        });
        handCards.AddOnUpdateItemsAction(OnCardsUpdated);

        // PauseMenu
        resumeButton = pauseMenuScreen.Q<Button>(USSElementNames.PAUSE_MENU_RESUME_BUTTON);
        settingsButton = pauseMenuScreen.Q<Button>(USSElementNames.PAUSE_MENU_SETTINGS_BUTTON);
        exitButton = pauseMenuScreen.Q<Button>(USSElementNames.PAUSE_MENU_EXIT_BUTTON);

    }

    #endregion


    #region Events
    [Header("Events")]
    [SerializeField] public UnityEvent PauseButtonClicked;

    [SerializeField] public UnityEvent PickCardsButtonClicked;
    [SerializeField] public UnityEvent UseCardButtonClicked;
    [SerializeField] public UnityEvent DropCardButtonClicked;


    [SerializeField] public UnityEvent ResumeButtonClicked;
    [SerializeField] public UnityEvent ExitButtonClicked;


    void SubscribeToEvents()
    {
        pickCardsButton.clicked += OnPickCardsBtnClicked;
        useCardButton.clicked += OnUseCardBtnClicked;
        dropCardButton.clicked += OnDropCardBtnClicked;

        pauseButton.clicked += OnPauseBtnClicked;
        resumeButton.clicked += OnResumeBtnClicked;
        exitButton.clicked += OnExitBtnClicked;

    }

    void UnsubscribeToEvents()
    {
        pickCardsButton.clicked -= OnPickCardsBtnClicked;
        useCardButton.clicked -= OnUseCardBtnClicked;
        dropCardButton.clicked -= OnDropCardBtnClicked;

        pauseButton.clicked -= OnPauseBtnClicked;
        resumeButton.clicked -= OnResumeBtnClicked;
        exitButton.clicked -= OnExitBtnClicked;

    }


    // Event callbacks
    void OnPickCardsBtnClicked()
    {
        GameplayManager.Instance.PickCards(deckCards.GetSelectedItems());
    }

    void OnUseCardBtnClicked()
    {
        GameplayManager.Instance.UseCard(handCards.GetSelectedItems()[0]);
    }

    void OnDropCardBtnClicked()
    {
        GameplayManager.Instance.DropCard(handCards.GetSelectedItems()[0]);
    }


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

    void OnCardsUpdated()
    {
        HideElement(pickCardsButton);
        HideElement(useCardButton);
        HideElement(dropCardButton);
    }

    #endregion


    #region MonoBehaviour
    void Awake()
    {
        SetupSingleton();
        SetupVisualElements();
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


    public void UpdateCards()
    {
        deckCards.UpdateItems(GameplayManager.Instance.DeckCards);
        handCards.UpdateItems(GameplayManager.Instance.CurrentPlayer.Cards);
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
