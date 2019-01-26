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
            }
            else
            {
                SpawnableItem.Reset(CANCEL);
                Destroy(gameObject);
            }
        }

        ConnectTiles(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDragging)
        {
            //Input.GetMouseButton(0)

            if (other.CompareTag("Tile") && !other.GetComponent<LevelTile>().IsBlocked)
            {
                _lastCollider = other;
            }
        }
    }

    private void OnMouseDown()
    {
        bool left = false, right = false, top = false, bottom = false;
        this.transform.Rotate(0, 0, -90);
        if(this.Left) { top = true; }
        if (this.Top) { right = true; }
        if(this.Right){ bottom = true; }
        if (this.Bottom) { left = true; }

        this.Left = left;
        this.Right = right;
        this.Top = top;
        this.Bottom = bottom;
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
        nextTileTop = null;
        nextTileBottom = null;
        nextTileLeft = null;
        nextTileRight = null;

        Vector3 euler = this.transform.eulerAngles;
        this.transform.Rotate(-euler);

        Vector3 left = this.transform.position + Vector3.left;
        Vector3 right = this.transform.position + Vector3.right;
        Vector3 top = this.transform.position + Vector3.up;
        Vector3 bottom = this.transform.position + Vector3.down;

        if (this.Left && _droppedTiles.ContainsKey(left) && _droppedTiles[left].Right)
        {
            nextTileLeft = _droppedTiles[left];
            if(connectNeighbours) nextTileLeft.ConnectTiles(false);
        }
        if (this.Right && _droppedTiles.ContainsKey(right) && _droppedTiles[right].Left)
        {
            nextTileRight = _droppedTiles[this.transform.position + Vector3.right];
            if (connectNeighbours) nextTileRight.ConnectTiles(false);
        }
        if (this.Bottom && _droppedTiles.ContainsKey(bottom) && _droppedTiles[bottom].Top)
        {
            nextTileBottom = _droppedTiles[bottom];
            if (connectNeighbours) nextTileBottom.ConnectTiles(false);
        }
        if (this.Top && _droppedTiles.ContainsKey(top) && _droppedTiles[top].Bottom)
        {
            nextTileTop = _droppedTiles[this.transform.position + Vector3.up];
            if (connectNeighbours) nextTileTop.ConnectTiles(false);
        }

        this.transform.Rotate(euler);
    }
}