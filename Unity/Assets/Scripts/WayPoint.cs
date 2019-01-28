using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public WayPoint[] NextPoints;

    public WayPoint GetNextWayPoint()
    {
        return NextPoints[Random.Range(0, NextPoints.Length - 1)];
    }

    public bool HasWayPoint()
    {
        return NextPoints.Length != 0;
    }

    void Start()
    {
        foreach(WayPoint wayPoint in NextPoints)
        {
            if(wayPoint == null)
            {
                Debug.Log("Fehlerhafter Wegpunkt: Nächster Wegpunkt ist nicht gesetzt!");
            }
        }
    }
}
