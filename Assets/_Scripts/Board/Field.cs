using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public int Index { get; set; }

    public Vector3 Position => transform.position;
    //public Quaternion Rotation => transform.rotation;
}
