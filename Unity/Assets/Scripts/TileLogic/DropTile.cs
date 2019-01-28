using System;
using System.Collections.Generic;
using UnityEngine;
using static ResetCause;

public class DropTile : MonoBehaviour
{
    public bool isDragging = false;
    public bool isPreset = false;

    public bool isGoal = false;

    public DropTile nextTileTop;
    public DropTile nextTileRight;
    public DropTile nextTileBottom;
    public DropTile nextTileLeft;

    private Collider2D _lastCollider;
    private Color _lastColliderColor;
    private float _lastColliderDistance = float.MaxValue;

    public SpawnableItem SpawnableItem;

    public bool Left;
    public bool Right;
    public bool Top;
    public bool Bottom;
    public AudioClip rotateSound;
    public float rotateSoundVolume = 2.5f;

    private bool mouseDown;

    public bool CanRotate = true;
    public bool EnemyOnTile = false;

    private LevelTile _connectedTile;

    private Level level;

    void Start()
    {
        level = GameObject.Find("Level").GetComponent<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isPreset) 
        {
           level._droppedTiles.Add(new Vector2(this.transform.position.x, this.transform.position.y), this);
           isPreset = false;
        }

        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;

            UpdateCollider();

            if (!Input.GetMouseButton(0))
            {
                if (_lastCollider != null)
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
                    level._droppedTiles.Add(new Vector2(this.transform.position.x, this.transform.position.y), this);

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
        }

        if (Input.GetMouseButton(0) && !mouseDown && !isDragging && CanRotate && !EnemyOnTile)
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

    private void UpdateCollider()
    {
        if(_lastCollider)
        {
            float currentDistance = getColliderDistance(_lastCollider);
            if(currentDistance > 1.5f)
            {
                ResetCollider();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isDragging && other.CompareTag("Tile") && !other.GetComponent<LevelTile>().IsBlocked)
        {
            float distance = getColliderDistance(other);

            if(other == _lastCollider) // update distance if the collider stays the same
            {
                _lastColliderDistance = distance;
            } 
            else if(distance < _lastColliderDistance) // otherwise check if a new one is closer
            {
                ResetCollider();
            
                _lastCollider = other;
                _lastColliderDistance = distance;

                Material mat = _lastCollider.gameObject.GetComponent<Renderer>().material; // highlight tile
                _lastColliderColor = mat.GetColor("_Color");
                mat.SetColor("_Color", new Color(1.25f, 1.25f, 1.25f));
            }
        }
    }
    
    public void ResetCollider()
    {
        if(_lastCollider)
        {
            Material mat = _lastCollider.gameObject.GetComponent<Renderer>().material;
            mat.SetColor("_Color", _lastColliderColor);
        }

        _lastCollider = null;
        _lastColliderDistance = float.MaxValue;
    }

    private float getColliderDistance(Collider2D other)
    {
        return (
            new Vector2(other.transform.position.x, other.transform.position.y) -
            new Vector2(transform.position.x, transform.position.y)
        ).magnitude;
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

    public void AddModifiers(PlayerBehaviour behaviour)
    {
        if (_connectedTile == null) return;

        behaviour.FearLevel *= _connectedTile.SpeedModifier;
        behaviour.FearLevel = Math.Max(behaviour.FearLevel, behaviour.FearLevelMax);

        if(_connectedTile.PortalTo != null && !_connectedTile.IsPortalExit())
        {
            if (level._droppedTiles.ContainsKey(_connectedTile.PortalTo.transform.position))
            {
                behaviour.TeleportTo(level._droppedTiles[_connectedTile.PortalTo.transform.position]);
            }
            else
            {
                behaviour.Die();
            }
        }
    }

    public bool IsNextTileValid(DropTile nextTile)
    {
        if(nextTile == null)
            return false;

        return nextTileLeft == nextTile || nextTileRight == nextTile 
            || nextTileTop  == nextTile || nextTileBottom == nextTile
            || this == nextTile;
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

        if (this.Left && level._droppedTiles.ContainsKey(left) && level._droppedTiles[left].Right)
        {
            nextTileLeft = level._droppedTiles[left];
            if(connectNeighbours) nextTileLeft.ConnectTiles(false);
        }
        if (this.Right && level._droppedTiles.ContainsKey(right) && level._droppedTiles[right].Left)
        {
            nextTileRight = level._droppedTiles[this.transform.position + Vector3.right];
            if (connectNeighbours) nextTileRight.ConnectTiles(false);
        }
        if (this.Bottom && level._droppedTiles.ContainsKey(bottom) && level._droppedTiles[bottom].Top)
        {
            nextTileBottom = level._droppedTiles[bottom];
            if (connectNeighbours) nextTileBottom.ConnectTiles(false);
        }
        if (this.Top && level._droppedTiles.ContainsKey(top) && level._droppedTiles[top].Bottom)
        {
            nextTileTop = level._droppedTiles[this.transform.position + Vector3.up];
            if (connectNeighbours) nextTileTop.ConnectTiles(false);
        }

        this.transform.Rotate(euler);
    }
}