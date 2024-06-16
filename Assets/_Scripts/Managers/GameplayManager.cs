using UnityEngine;


public class GameplayManager : MonoBehaviour
{
    #region Singleton
    static GameplayManager instance;
    public static GameplayManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("No GameplayManager instance");
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

    public Transform[] players = new Transform[2];
    public PlayerCamera playerCamera;

    public int PlayerTurn { get; private set; }

    void Awake()
    {
        SetupSingleton();
    }

    void Start()
    {
        PlayerTurn = 1;
        playerCamera.ChangePlayer(players[PlayerTurn]);
    }


    public void EndTurn(Pawn sender)
    {
        if (sender.id != PlayerTurn) return;

        int nextPlayerId = (PlayerTurn + 1) % players.Length;
        PlayerTurn = nextPlayerId;

        playerCamera.ChangePlayer(players[PlayerTurn]);
    }
}
