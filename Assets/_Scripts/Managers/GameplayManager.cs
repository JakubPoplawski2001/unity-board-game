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


    void SpawPawns()
    {
        int playersQuantity = 3;
        for (int i = 0; i < playersQuantity; i++)
        {
            // ToDo: Change spawn position (i => 0)
            var playerGO = Instantiate(pawnPrefab, Route.FieldsList[0].position + Route.Offset[i], Quaternion.identity);
            var playerPawn = playerGO.GetComponent<Pawn>();
            playerPawn.Id = i;
            Players.Add(playerPawn);
            //Players[i] = playerPawn;
        }
    }


    void Awake()
    {
        SetupSingleton();
        SpawPawns();
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
}
