using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("No GameManager instance");
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

        // Keep manager between scenes
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region State Machine
    public BaseGameState CurrentState;
    public Dictionary<string, BaseGameState> GameStates;
    public event Action<BaseGameState> OnGameStateChanged;


    /// <summary>
    /// Changes Current State to targeted, while handling transition and ivoking OnGameStateChange event
    /// </summary>
    /// <param name="targetState"> Use construct GameManager.Instance.GameStates[nameof(BaseGameState)] </param>
    /// <returns></returns>
    public bool ChangeState(BaseGameState targetState)
    {
        // Don't change state if already in targeted state
        if (CurrentState == targetState) return false;

        // Leave previous state
        CurrentState?.ExitState();

        // Change state
        CurrentState = targetState;
        OnGameStateChanged?.Invoke(CurrentState);
        CurrentState.EnterState();
        return true;
    }

    /// <summary>
    /// Changes Current State to targeted, while handling transition and ivoking OnGameStateChange event
    /// </summary>
    /// <param name="targetStateName"> Use construct nameof(BaseGameState) </param>
    /// <returns></returns>
    public bool ChangeState(string targetStateName)
    {
        BaseGameState targetState = GameStates[targetStateName];
        return ChangeState(targetState);
    }

    void SetupStateMachine()
    {
        GameStates = new Dictionary<string, BaseGameState>
        {
            { nameof(MainMenuGameState), new MainMenuGameState() },
            { nameof(GameplayGameState), new GameplayGameState() },
            //{ nameof(PauseMenuGameState), new PauseMenuGameState() },
            { nameof(GameOverGameState), new GameOverGameState() },
        };

    }

    #endregion


    void Awake()
    {
        SetupSingleton();
        SetupStateMachine();

    }

    void Start()
    {
        // Set starting state to Main Menu
        ChangeState(nameof(MainMenuGameState));
    }
}
