using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Route : MonoBehaviour
{
    [SerializeField] private List<Field> fields = new List<Field>();
    public List<Field> Fields { get => fields; }

    [SerializeField] bool isLooped = true;
    public bool IsLooped { get => isLooped; }

    [SerializeField] List<Castle> castles = new List<Castle>();
    public List<Castle> Castles { get => castles; }

    [SerializeField] float offsetValue = 2;
    public Vector3[] Offset
    {
        get
        {
            return new Vector3[4]
            {
                new Vector3(offsetValue, 0, offsetValue),
                new Vector3(-offsetValue, 0, offsetValue),
                new Vector3(offsetValue, 0, -offsetValue),
                new Vector3(-offsetValue, 0, -offsetValue),
            };
        }
    }


    void Awake()
    {
        UpdateFields();
    }

    void Start()
    {
        SetupCastles();
    }


    #region Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        UpdateFields();

        DrawConnections();
        DrawStart();
        DrawOffset(1);
    }

    void DrawOffset(int fieldIndex)
    {
        Gizmos.color = Color.magenta;

        foreach (var offset in Offset)
        {
            Gizmos.DrawRay(Fields[fieldIndex].Position + offset, Vector3.up);
        }
    }

    void DrawStart()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Fields[0].Position, new Vector3(0.5f, 2, 2));
    }

    void DrawConnections()
    {
        for (int i = 0; i < Fields.Count; i++)
        {
            Vector3 currentPos = Fields[i].Position;

            // Skip first field
            if (i <= 0) continue;

            Vector3 previousPos = Fields[i - 1].Position;

            Gizmos.DrawLine(previousPos, currentPos);
        }

        if (IsLooped)
        {
            Gizmos.DrawLine(Fields[Fields.Count - 1].Position, Fields[0].Position);
        }
    }
    #endregion


    public void SetupCastles()
    {
        var players = GameplayManager.Instance.Players;
        var castles = GetComponentsInChildren<Castle>();

        if (castles.Length < players.Count)
        {
            throw new IndexOutOfRangeException("Not enough Castles for all Players");
        }

        for (int i = 0; i < players.Count; i++)
        {
            castles[i].Owner = players[i];
            castles[i].Name = $"{players[i].name}'s Castle";

            Castles.Add(castles[i]);
        }
    }

    void UpdateFields()
    {
        fields.Clear();
        var fieldsArray = GetComponentsInChildren<Field>();
        for (int i = 0; i<fieldsArray.Length; i++)
        {
            var field = fieldsArray[i];
            field.Index = i;
            fields.Add(field);
        }
    }
}
