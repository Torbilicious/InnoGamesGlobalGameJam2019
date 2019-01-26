﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    public bool IsBlocked = false;
    public GameObject Block;

    void Start()
    {
        //Be sure that Tile is snapped to Grid
        this.transform.position = new Vector3(
            (float)Math.Round(this.transform.position.x),
            (float)Math.Round(this.transform.position.y),
            (float)Math.Round(this.transform.position.z));
    }

    void Update()
    {
        Block.SetActive(IsBlocked);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
