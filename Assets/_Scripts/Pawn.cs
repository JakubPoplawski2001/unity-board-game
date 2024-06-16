using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public Route currentRout;
    public int id;

    int routeIndex;

    public int Steps;
    protected bool isMoving;


    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        //{
        //    Steps = UnityEngine.Random.Range(1, 7);
        //    Debug.Log($"Rolled {Steps}");

        //    try
        //    {
        //        StartCoroutine(Move());
        //    }
        //    catch (ArgumentOutOfRangeException e) { 
        //        Debug.LogError("Out of range error: " + e.Message);
        //    }
        //}
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
            if (routeIndex == currentRout.FieldsList.Count - 1)
            {
                if (currentRout.isLooped)
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

            Vector3 nextPos = currentRout.FieldsList[routeIndex + 1].position;
            
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
