using UnityEngine;


public abstract class BaseGameState
{
    /// <summary>
    /// Actions to setup state
    /// </summary>
    public abstract void EnterState();

    /// <summary>
    /// UpdateState
    /// </summary>
    public abstract void UpdateState();

    /// <summary>
    /// Actions to close and exit state
    /// </summary>
    public abstract void ExitState();

    /// <summary>
    /// Returns TRUE if this state is equal to target state in GameManager.GameStates[stateName]
    /// </summary>
    /// <param name="stateName"> Use construct nameof(BaseGameState) </param>
    /// <returns></returns>
    public bool Equals(string stateName)
    {
        return this == GameManager.Instance.GameStates[stateName];
    }
}