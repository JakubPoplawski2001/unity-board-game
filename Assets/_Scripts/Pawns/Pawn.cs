using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    Route currentRout;
    // ToDo: Change Id to private
    public int Id {  get; set; }

    int routeIndex;

    public int Steps;
    protected bool isMoving;


    public List<ICard> Cards;
    public Castle Castle;


    void Start()
    {
        currentRout = GameplayManager.Instance.Route;
        Cards = new List<ICard>();
    }

    void Update()
    {

    }


    protected IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }

        isMoving = true;

        while (Steps > 0)
        {
            // Handle OutOfRange fields
            if (routeIndex == currentRout.Fields.Count - 1)
            {
                if (currentRout.IsLooped)
                {
                    // Loop
                    routeIndex = -1;
                }
                else
                {
                    // Finish
                    Debug.Log("You are on the last field");
                    break;
                }

            }

            Vector3 nextPos = currentRout.Fields[routeIndex + 1].Position + currentRout.Offset[Id];
            
            // Wait until next position is reached
            while (MoveToTarget(nextPos))
            {
                yield return null;
            }

            // Stop on field
            //yield return new WaitForSeconds(0.1f);
            Steps--;
            routeIndex++;
        }

        GameplayManager.Instance.EndTurn(this);

        isMoving = false;
    }

    bool MoveToTarget(Vector3 target)
    {
        float speed = 6f * Time.deltaTime;

        Vector3 movePos = Vector3.MoveTowards(transform.position, target, speed);

        // Rotate
        RotateToTarget(target);

        return target != (transform.position = movePos);
    }

    void RotateToTarget(Vector3 target)
    {
        float speed = 6f * Time.deltaTime;

        Vector3 relativePos = target - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(relativePos);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed);
    }


    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawRay((transform.position + Vector3.up), transform.forward * 5);
    }
}
