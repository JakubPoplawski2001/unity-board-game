using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] fields;
    public List<Transform> FieldsList = new List<Transform>();
    public Vector3[] Offset = new Vector3[4];
    public bool isLooped = true;


    void Awake()
    {
        GenerateOffset();
        UpdateFields();
    }


    void GenerateOffset()
    {
        float offset = 2;
        Offset = new Vector3[4]
        {
            new Vector3(offset, 0, offset),
            new Vector3(-offset, 0, offset),
            new Vector3(offset, 0, -offset),
            new Vector3(-offset, 0, -offset)
        };
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        UpdateFields();

        DrawConnections();

        Gizmos.color= Color.blue;
        Gizmos.DrawWireCube(FieldsList[0].position, new Vector3(0.5f, 2, 2));
    }

    void DrawConnections()
    {
        for (int i = 0; i < FieldsList.Count; i++)
        {
            Vector3 currentPos = FieldsList[i].position;

            // Skip first field
            if (i <= 0) continue;

            Vector3 previousPos = FieldsList[i - 1].position;

            Gizmos.DrawLine(previousPos, currentPos);
        }

        if (isLooped)
        {
            Gizmos.DrawLine(FieldsList[FieldsList.Count - 1].position, FieldsList[0].position);
        }
    }


    void UpdateFields()
    {
        FieldsList.Clear();

        fields = GetComponentsInChildren<Transform>();

        foreach (Transform fieldTransform in fields)
        {
            // Skip parent object
            if (fieldTransform == this.transform) continue;

            FieldsList.Add(fieldTransform);
        }
    }
}
