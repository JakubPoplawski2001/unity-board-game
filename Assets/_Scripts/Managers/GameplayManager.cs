using System.Collections.Generic;
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

    public PlayerCamera PlayerCamera;
    public Route Route;
    [SerializeField] GameObject pawnPrefab;
    public List<Pawn> Players = new List<Pawn>();


    public int PlayerTurn { get; private set; }


    void SpawnPawns()
    {
        int playersQuantity = 3;
        for (int i = 0; i < playersQuantity; i++)
        {
            // ToDo: Change spawn position (i => 0)
            var playerGO = Instantiate(pawnPrefab, Route.Fields[0].Position + Route.Offset[i], Quaternion.identity);
            var playerPawn = playerGO.GetComponent<Pawn>();
            playerPawn.Id = i;
            Players.Add(playerPawn);
            //Players[i] = playerPawn;
        }
    }


    void Awake()
    {
        SetupSingleton();
        SpawnPawns();
    }

    void Start()
    {
        PlayerTurn = 0;
        PlayerCamera.ChangePlayer(Players[PlayerTurn].transform);
    }


    public void EndTurn(Pawn sender)
    {
        if (sender.Id != PlayerTurn) return;

        int nextPlayerId = (PlayerTurn + 1) % Players.Count;
        PlayerTurn = nextPlayerId;

        PlayerCamera.ChangePlayer(Players[PlayerTurn].transform);
    }

    void Turn()
    {
        // Get current Player

        // Check if Player has any cards "in hand"
        // if (Player.Cards is not empty) -> return valid player Actions

        // Wait for PlayerAction:
        // if PlayerActions has only one action then perform it (not necessary)
        // a) Pick Card
        // b) Use Card (if has valid cards - look up)
        
        // Perform selected action and following e.g. CardAction
        // Apply FieldAction (only of the new field / after move)

        // End current players turn

        // Check Win state?

        // Starts new turn of next player
    }
}
