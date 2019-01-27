using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Dictionary<Vector2, DropTile> _droppedTiles = new Dictionary<Vector2, DropTile>();
    public int nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        GameState.Reset();
        GameState.nexLevel = nextLevel;
        _droppedTiles = new Dictionary<Vector2, DropTile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
