using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public Transform[] players = new Transform[2];
    public PlayerCamera playerCamera;

    public int PlayerTurn { get; private set; }

    void Awake()
    {
        if (instance == null) { instance = this; }
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
