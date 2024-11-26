using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainMenuView
{
    
    public MainMenuView(VisualElement screen)
    {
        root = screen;
        SetupVisualElements();
    }

    #region VisualElements
    VisualElement root;
    Button playButton;
    Button settingsButton;
    Button exitButton;


    void SetupVisualElements()
    {
        playButton = root.Q<Button>(USSElementNames.MAIN_MENU_PLAY_BUTTON);
        settingsButton = root.Q<Button>(USSElementNames.MAIN_MENU_SETTINGS_BUTTON);
        exitButton = root.Q<Button>(USSElementNames.MAIN_MENU_EXIT_BUTTON);
    }

    #endregion


    #region Events
    //public UnityEvent PlayButtonClicked;
    public Action PlayButtonClicked;
    public UnityEvent ExitButtonClicked;


    void SubscribeToEvents()
    {
        //playButton.clicked += PlayButtonClicked;
        //PlayButtonClicked += OnPlayBtnClicked;
        playButton.clicked += OnPlayBtnClicked;
        exitButton.clicked += OnExitBtnClicked;
    }

    void UnsubscribeToEvents()
    {
        playButton.clicked -= OnPlayBtnClicked;
        exitButton.clicked -= OnExitBtnClicked;
    }


    void OnPlayBtnClicked()
    {
        PlayButtonClicked?.Invoke();
        // ToDo: How to hide other screens?
        // Get UIManager
        // 1. in UIManager add method to swap screens ?
        // 2. from UIManager get Views to hide/show ?
        // 3. move entirely to UIManager
    }

    void OnExitBtnClicked()
    {
        // ToDo: Should be moved to GameManager

        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    #endregion


    public void Show()
    {
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        root.style.display = DisplayStyle.None;
    }
}
