using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResetCause;

public class DropTile : MonoBehaviour
{
    private static Dictionary<Vector2, DropTile> _droppedTiles = new Dictionary<Vector2, DropTile>();

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
    public AudioClip rotateSound;
    public float rotateSoundVolume = 2.5f;

    private bool mouseDown;

    public bool CanRotate = true;

    private LevelTile _connectedTile;

    void Start()
    {
        if(isPreset) 
        {
           _droppedTiles.Add(new Vector2(this.transform.position.x, this.transform.position.y), this);
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
        if ( (isDragging && !Input.GetMouseButton(0)))
        {
            if (_lastCollider != null && Collides())
            {
                _lastCollider.GetComponent<LevelTile>().HandleTileSet();
                this.transform.position = _lastCollider.transform.position;
                _connectedTile = _lastCollider.gameObject.GetComponent<LevelTile>();
                _lastCollider.gameObject.SetActive(false);
                isDragging = false;
                if (SpawnableItem != null)
                {
                    SpawnableItem.Reset(PLACED);
                }
                _droppedTiles.Add(new Vector2(this.transform.position.x, this.transform.position.y), this);

                if(_connectedTile.PortalTo != null)
                {
                    //TODO: Portal-Animation aktivieren
                }
            }
            else
            {
                SpawnableItem.Reset(CANCEL);
                Destroy(gameObject);
            }
        }

        if (Input.GetMouseButton(0) && !mouseDown && !isDragging && CanRotate)
        {
            Vector3 stw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Rect rect = new Rect(gameObject.transform.position - gameObject.transform.localScale / 2, gameObject.transform.localScale);
            if (rect.Contains(stw)) OnManualMouseDown();
            mouseDown = true;
        }
        else if(!Input.GetMouseButton(0) && mouseDown)
        {
            mouseDown = false;
        }
        ConnectTiles(false);
    }

    private bool Collides()
    {
        Vector3 center = GetComponent<Renderer>().bounds.center;
        Vector3 diff = center - _lastCollider.gameObject.transform.position;
        return diff.x < 0.3f && diff.y < 0.3f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDragging)
        {
            //Input.GetMouseButton(0)
            //Debug.Log(other.CompareTag("Tile") && !other.GetComponent<LevelTile>().IsBlocked);
            if (other.CompareTag("Tile") && !other.GetComponent<LevelTile>().IsBlocked)
            {
                _lastCollider = other;
            }
        }
    }

    private void OnManualMouseDown()
    {
        if (rotateSound)
            AudioSource.PlayClipAtPoint(rotateSound, transform.position, rotateSoundVolume);

        bool left = false, right = false, top = false, bottom = false;
        this.transform.Rotate(0, 0, -90);
        if (this.Left) { top = true; }
        if (this.Top) { right = true; }
        if (this.Right) { bottom = true; }
        if (this.Bottom) { left = true; }

        this.Left = left;
        this.Right = right;
        this.Top = top;
        this.Bottom = bottom;
    }

    public void AddModifiers(EnemyBehaviour behaviour)
    {
        if (_connectedTile == null) return;

        behaviour.FearLevel *= _connectedTile.SpeedModifier;
        behaviour.FearLevel = Math.Max(behaviour.FearLevel, behaviour.FearLevelMax);

        if(_connectedTile.PortalTo != null && !_connectedTile.IsPortalExit())
        {
            if (_droppedTiles.ContainsKey(_connectedTile.PortalTo.transform.position))
            {
                behaviour.TeleportTo(_droppedTiles[_connectedTile.PortalTo.transform.position]);
            }
            else
            {
                behaviour.Die();
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
            return null;
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

        Vector2 posNoZ = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 left = posNoZ + Vector2.left;
        Vector2 right = posNoZ + Vector2.right;
        Vector2 top = posNoZ + Vector2.up;
        Vector2 bottom = posNoZ + Vector2.down;

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