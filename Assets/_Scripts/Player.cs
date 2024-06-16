using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Pawn
{
    void Update()
    {
        if (GameplayManager.Instance.PlayerTurn != id) return;

        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            Steps = UnityEngine.Random.Range(1, 7);
            Debug.Log($"Rolled {Steps}");

            try
            {
                StartCoroutine(Move());
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.LogError("Out of range error: " + e.Message);
            }
        }
    }


}
