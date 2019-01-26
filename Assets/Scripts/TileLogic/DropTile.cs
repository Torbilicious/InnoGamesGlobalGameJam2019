using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResetCause;

public class DropTile : MonoBehaviour
{
    private static Dictionary<Vector3, DropTile> _droppedTiles = new Dictionary<Vector3, DropTile>();

    public bool isDragging = false;
    public bool isPreset = false;

    public DropTile nextTileTop;
    public DropTile nextTileRight;
    public DropTile nextTileBottom;
    public DropTile nextTileLeft;

    private Collider2D _lastCollider;

    public SpawnableItem SpawnableItem;

    public bool Left;
    public bool Right;
    public bool Top;
    public bool Bottom;

    void Start()
    {
        if(isPreset) 
        {
            _droppedTiles.Add(this.transform.position, this);
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }

        if (isDragging && !Input.GetMouseButton(0))
        {
            if (_lastCollider != null && _lastCollider.bounds.Intersects(GetComponent<Collider2D>().bounds))
            {
                this.transform.position = _lastCollider.transform.position;
                Destroy(_lastCollider.gameObject);
                isDragging = false;
                if (SpawnableItem != null)
                {
                    SpawnableItem.Reset(PLACED);
                }
                _droppedTiles.Add(this.transform.position, this);
                ConnectTiles(true);
            }
            else
            {
                SpawnableItem.Reset(CANCEL);
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

    public DropTile GetRandomNextTile(DropTile ignoreTile) {
        
        List<DropTile> tileList = new List<DropTile>();
        if (nextTileLeft != null && nextTileLeft != ignoreTile) tileList.Add(nextTileLeft);
        if (nextTileRight != null && nextTileRight != ignoreTile) tileList.Add(nextTileRight);
        if (nextTileTop != null && nextTileTop != ignoreTile) tileList.Add(nextTileTop);
        if (nextTileBottom != null && nextTileBottom != ignoreTile) tileList.Add(nextTileBottom);

        if(tileList.Count == 0) {
            tileList.Add(ignoreTile); //TODO: game over! The player goes back for now
        }

        System.Random rnd = new System.Random(); // choose a random tile
        int index = rnd.Next(0, tileList.Count);
        return tileList[index];
    }

    public void ConnectTiles(bool connectNeighbours)
    {
        Vector3 euler = this.transform.eulerAngles;
        this.transform.Rotate(-euler);

        if (this.Left && _droppedTiles.ContainsKey(this.transform.position + Vector3.left))
        {
            nextTileLeft = _droppedTiles[this.transform.position + Vector3.left];
            if(connectNeighbours) nextTileLeft.ConnectTiles(false);
        }
        if (this.Right && _droppedTiles.ContainsKey(this.transform.position + Vector3.right))
        {
            nextTileRight = _droppedTiles[this.transform.position + Vector3.right];
            if (connectNeighbours) nextTileRight.ConnectTiles(false);
        }
        if (this.Bottom && _droppedTiles.ContainsKey(this.transform.position + Vector3.down))
        {
            nextTileBottom = _droppedTiles[this.transform.position + Vector3.down];
            if (connectNeighbours) nextTileBottom.ConnectTiles(false);
        }
        if (this.Top && _droppedTiles.ContainsKey(this.transform.position + Vector3.up))
        {
            nextTileTop = _droppedTiles[this.transform.position + Vector3.up];
            if (connectNeighbours) nextTileTop.ConnectTiles(false);
        }

        this.transform.Rotate(euler);
    }
}