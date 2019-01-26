using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    void Start()
    {
        //Be sure that Tile is snapped to Grid
        this.transform.position = new Vector3(
            (float)Math.Round(this.transform.position.x),
            (float)Math.Round(this.transform.position.y),
            (float)Math.Round(this.transform.position.z));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
