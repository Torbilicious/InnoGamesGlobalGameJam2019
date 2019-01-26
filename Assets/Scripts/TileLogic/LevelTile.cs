using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    public LevelTile nextTileTop;
    public LevelTile nextTileRight;
    public LevelTile nextTileBottom;
    public LevelTile nextTileLeft;

    private List<LevelTile> existingTiles;

    void Start()
    {
        //Be sure that Tile is snapped to Grid
        this.transform.position = new Vector3(
            (float)Math.Round(this.transform.position.x),
            (float)Math.Round(this.transform.position.y),
            (float)Math.Round(this.transform.position.z));

        if(nextTileTop)existingTiles.Add(nextTileTop);
        if(nextTileRight)existingTiles.Add(nextTileRight);
        if(nextTileBottom)existingTiles.Add(nextTileBottom);
        if(nextTileLeft)existingTiles.Add(nextTileLeft);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
/*
    public LevelTile getRandomNextTile() {
        System.Random rnd = new System.Random();
    }
    */
}
