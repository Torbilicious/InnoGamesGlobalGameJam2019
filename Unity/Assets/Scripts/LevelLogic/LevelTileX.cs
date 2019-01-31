using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public partial class LevelTileX : MonoBehaviour
{
    private const string nameIdentifier = "LevelTile ";

    private bool _collidesWithOtherLevelTile = false;
    private Collider2D _lastCollider;
    private bool _mouseOver = false;

    private TileData _data;
    private float _synchronizationTimer = 1.0f;

    public GameObject Editor;

    public Portal Portal;

    public LevelTileGameMechanics GameMechanic;

    public DragableObject DragMechanic;

    public SpriteRenderer SpriteRenderer;

    public LevelTileX()
    {
        _data = new TileData();
    }

    // Start is called before the first frame update
    void Start()
    {
        Editor.SetActive(false);
    }

    void Update()
    {
        //Editor stuff
        if(Input.GetMouseButtonDown(1) && _mouseOver && LevelManager.EditorEnabled)
        {
            Editor.SetActive(!Editor.activeSelf);
        }
        if(Editor.activeSelf)
        {
            Editor.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(transform.lossyScale.x * 1.5f, 0, 0));
            Editor.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if ((_synchronizationTimer -= Time.deltaTime) < 0)
        {
            SynchronizeData();
            _synchronizationTimer = 60.0f;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<LevelTileX>() != null &&
            (int)other.gameObject.transform.position.x == (int)this.gameObject.transform.position.x &&
                (int)other.gameObject.transform.position.y == (int)this.gameObject.transform.position.y)
        {
            _collidesWithOtherLevelTile = true;
            _lastCollider = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == _lastCollider)
            _collidesWithOtherLevelTile = false;
    }

    void OnMouseOver()
    {
        _mouseOver = true;
    }

    void OnMouseExit()
    {
        _mouseOver = false;
    }

    private LevelTileX FindTileByPosition(int x, int y)
    {
        return GameObject.Find(nameIdentifier + x + ":" + y)?.GetComponent<LevelTileX>();
    }

    public void SetTileData(TileData data, bool merge = false)
    {
        if (merge)
        {
            //Override all data with the attribute DispenserOverride
            foreach(var property in typeof(TileData).GetProperties().Where(prop => prop.IsDefined(typeof(DispenserOverride), false)))
            {
                property.SetValue(_data, property.GetValue(data));
            }
        }
        else
        {
            _data = data;
        }
        transform.position = new Vector3(_data.PosX, _data.PosY, 0);
        SynchronizeData();
    }

    public TileData GetTileData()
    {
        return _data;
    }

    public void OnDragStop(DragEventArgs args)
    {
        //This item is used a a dispenser and gives its data to another tile
        if (_data.IsDispenser && _collidesWithOtherLevelTile)
        {
            LevelTileX existingTile = _lastCollider.gameObject.GetComponent<LevelTileX>();
            existingTile.SetTileData(_data, true);
        }
        //If it is not a dispenser, perform an additional collider check
        else if (_collidesWithOtherLevelTile &&
            ((int)_lastCollider.gameObject.transform.position.x != (int)this.gameObject.transform.position.x ||
            (int)_lastCollider.gameObject.transform.position.y != (int)this.gameObject.transform.position.y))
        {
            _collidesWithOtherLevelTile = false;
        }

        //Set info for the object that created this tile
        DragMechanic.DragSuccessful = !_collidesWithOtherLevelTile;

        //If it is not colliding, drop it on to the level grid
        if (!_collidesWithOtherLevelTile)
        {
            _data.PosX = (int)transform.position.x;
            _data.PosY = (int)transform.position.y;
            DragMechanic.enabled = false;
            SynchronizeData();
        }
    }
}
