﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResetCause;

public class DropTile : MonoBehaviour
{
    bool isDragging = true;

    public DropTile nextTileTop;
    public DropTile nextTileRight;
    public DropTile nextTileBottom;
    public DropTile nextTileLeft;

    private List<DropTile> existingTiles;

    private Collider2D _lastCollider;

    public SpawnableItem SpawnableItem;

    void Start()
    {
        if(nextTileTop)existingTiles.Add(nextTileTop);
        if(nextTileRight)existingTiles.Add(nextTileRight);
        if(nextTileBottom)existingTiles.Add(nextTileBottom);
        if(nextTileLeft)existingTiles.Add(nextTileLeft);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isDragging);
        
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }

        if (isDragging && !Input.GetMouseButton(0))
        {
            if (_lastCollider != null)
            {
                this.transform.position = _lastCollider.transform.position;
                Destroy(_lastCollider.gameObject);
                isDragging = false;
                SpawnableItem.reset(PLACED);
            }
            else
            {
                SpawnableItem.reset(CANCEL);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDragging)
        {
            //Input.GetMouseButton(0)

            if (other.CompareTag("Tile"))
            {
                _lastCollider = other;
            }
        }
    }

    public DropTile getRandomNextTile() {
        System.Random rnd = new System.Random();
        return null;
    }
}