using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;

    public PointInTime(Vector3 newPosition, Quaternion newRotation)
    {
        position = newPosition;
        rotation = newRotation;
    }
}
