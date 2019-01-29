using System;
using UnityEngine;

[ExecuteAlways]
public class SnapToGrid : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(
                (float)Math.Round(transform.position.x, MidpointRounding.AwayFromZero),
                (float)Math.Round(transform.position.y, MidpointRounding.AwayFromZero),
                (float)Math.Round(transform.position.z, MidpointRounding.AwayFromZero));
    }
}
